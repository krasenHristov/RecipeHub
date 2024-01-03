using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class UserRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        this._configuration = configuration;

        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        if (env == "Testing")
        {
            _connectionString = "TestConnection";
        }
        else if (env == "Production")
        {
            _connectionString = "ProductionConnection";
        }
        else
        {
            _connectionString = "DefaultConnection";
        }
    }

    public async Task<GetUserDto?> GetUserByUsername(string username)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));
        var sql = "SELECT * FROM \"User\" WHERE \"Username\" = @Username";

        return await connection.QueryFirstOrDefaultAsync<GetUserDto>(sql, new { Username = username });
    }

    public async Task<GetUserDto?> GetUserById(int id)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));
        var sql = "SELECT * FROM \"User\" WHERE \"UserId\" = @UserId";

        return await connection.QueryFirstOrDefaultAsync<GetUserDto>(sql, new { UserId = id });
    }

    public async Task<string> RegisterUser(User user)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        user.Password = HashPassword(user.Password!);

        var parameters = new DynamicParameters();
        parameters.Add("Username", user.Username);
        parameters.Add("Name", user.Name);
        parameters.Add("ProfileImg", user.ProfileImg);
        parameters.Add("Password", user.Password);
        parameters.Add("Status", user.Status);
        parameters.Add("Bio", user.Bio);

        var sql = "INSERT INTO \"User\" " +
                  "(\"Username\", \"Name\", \"ProfileImg\", \"Password\", \"Status\", \"Bio\") " +
                  "VALUES (@Username, @Name, @ProfileImg, @Password, @Status, @Bio) RETURNING *";
        var newUser = await connection.QueryAsync<User>(sql, parameters);

        if (newUser == null)
        {
            throw new Exception("User not created");
        }

        return GenerateJwtToken(await GetUserByUsername(user.Username!));
    }

    public async Task<string> SignUserIn(string username, string password)
    {
        var user = await GetUserByUsername(username);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (!CheckPassword(password, user.Password!))
        {
            throw new Exception("Invalid password");
        }

        return GenerateJwtToken(await GetUserByUsername(username));
    }

    private string GenerateJwtToken(GetUserDto? user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user!.UserId.ToString()!),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username!),
            new Claim(JwtRegisteredClaimNames.NameId, user.Name!),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        var hasher = new PasswordHasher<User>();
        return hasher.HashPassword(null!, password);
    }

    private bool CheckPassword(string password, string hashedPassword)
    {
        var hasher = new PasswordHasher<User>();
        return hasher.VerifyHashedPassword(null!, hashedPassword, password) != PasswordVerificationResult.Failed;
    }
}

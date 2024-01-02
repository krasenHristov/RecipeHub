using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class UserRepository(IConfiguration configuration)
{
    public async Task<GetUserByUsernameDto?> GetUserByUsername(string username)
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
        var sql = "SELECT * FROM \"User\" WHERE \"Username\" = @Username";

        return await connection.QueryFirstOrDefaultAsync<GetUserByUsernameDto>(sql, new { Username = username });
    }

    public async Task<string> RegisterUser(User user)
    {
        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));

        var checkUser = await GetUserByUsername(user.Username!);

        if (checkUser != null)
        {
            throw new Exception("Username already exists");
        }

        if (user.Password != null)
        {
            user.Password = HashPassword(user.Password);

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

    private string GenerateJwtToken(GetUserByUsernameDto? user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? string.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user!.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username!),
            new Claim(JwtRegisteredClaimNames.NameId, user.Name!),
        };

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        var hasher = new PasswordHasher<User>();
        return hasher.HashPassword(null, password);
    }

    private bool CheckPassword(string password, string hashedPassword)
    {
        var hasher = new PasswordHasher<User>();
        return hasher.VerifyHashedPassword(null, hashedPassword, password) != PasswordVerificationResult.Failed;
    }
}

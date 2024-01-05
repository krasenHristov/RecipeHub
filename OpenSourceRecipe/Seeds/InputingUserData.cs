using OpenSourceRecipes.Utils;
using OpenSourceRecipes.Services;
using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class SeedUserData(IConfiguration configuration)
    {
        public async Task<IEnumerable<MyUserObject>> InsertIntoUser()
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString("TestConnection"));
            ReadUserFunc readUser = new ReadUserFunc();

            var passwordStuff = new UserRepository(configuration);

            List<MyUserObject> users = readUser.ReadUserFile();

            List<MyUserObject> insertedUsers = new List<MyUserObject>();

            Console.WriteLine("about to insert Into Users");
            foreach (var user in users)
            {
                string query = $"INSERT INTO \"User\" " +
                                "(\"Username\", \"Name\", \"Password\", \"ProfileImg\", \"Status\", \"Bio\") " +
                                $"VALUES ('{user.Username}', '{user.Name}', '{passwordStuff.HashPassword(user.Password)}', '{user.ProfileImg}', '{user.Status}', '{user.Bio}') " +
                                "RETURNING *;";
                var insertedUser = await connection.QueryFirstOrDefaultAsync<MyUserObject>(query);
                if (insertedUser != null) insertedUsers.Add(insertedUser);
            }
            return insertedUsers;
        }
    }
}

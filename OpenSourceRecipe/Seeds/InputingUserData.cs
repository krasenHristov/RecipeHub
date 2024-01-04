using FluentMigrator;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OpenSourceRecipes.Utils;
using Dapper;
using Npgsql;

namespace OpenSourceRecipes.Seeds
{
    public class SeedUserData(IConfiguration configuration)
    {
        public async Task<IEnumerable<MyUserObject>> InsertIntoUser()
        {
            await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            ReadUserFunc ReadUser = new ReadUserFunc();

            List<MyUserObject> Users = ReadUser.ReadUserFile();
            List<MyUserObject> insertedUsers = new List<MyUserObject>();
            
            Console.WriteLine("about to insert Into Users");
            foreach (var user in Users)
            {
                //if theres an error with user check this VERY high likely hood that status will be your issue
                string query = $"INSERT INTO \"User\" " +
                                "(\"Username\", \"Name\", \"Password\", \"ProfileImg\", \"Status\", \"Bio\") " +
                                $"VALUES ('{user.Username}', '{user.Name}', '{user.Password}', '{user.ProfileImg}', '{user.Status}', '{user.Bio}') " +
                                "RETURNING *;";
                var insertedUser = await connection.QueryFirstOrDefaultAsync<MyUserObject>(query);
                insertedUsers.Add(insertedUser);
            }

            return insertedUsers;
        }
    }
}
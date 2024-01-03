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
    public class SeedFoodData(IConfiguration configuration)
    {
        public async void InsertIntoFood()
        {
            // await using var connection = new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
            // ReadUserFunc ReadUser = new ReadUserFunc();
            // List<MyUserObject> Users = ReadUser.ReadUserFile();

            // List<MyUserObject> insertedUsers = new List<MyUserObject>();

            // foreach (var user in Users)
            // {
            //     string query = $"INSERT INTO \"User\" " +
            //                     "(\"Username\", \"Name\", \"Password\", \"ProfileImg\", \"Status\", \"Bio\") " +
            //                     $"VALUES ('{user.Username}', '{user.Name}', '{user.Password}', '{user.ProfileImg}', '{user.Status}', '{user.Bio}') " +
            //                     "RETURNING *;";
            //     var insertedUser = await connection.QueryFirstOrDefaultAsync<MyUserObject>(query);
            //     insertedUsers.Add(insertedUser);
            // }

            // return insertedUsers;
        }
    }
}
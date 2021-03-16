using Dapper;
using DapperLesson.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperLesson.Data
{
    public class PlayerDataAccess : DataAccess<Player>
    {
        public override ICollection<Player> GetAll()
        {
            using (var connection = new SqlConnection())
            {
                return connection.Query<Player>("Select * from Players").ToList();
            }
        }
        public override void Insert(Player player)
        {
            using (var connection = new SqlConnection())
            {
                connection.Execute("Insert into Players values (@Id, @FullName, @Number)", player);
            }
        }
        public override void Update(Player player)
        {
            using (var connection = new SqlConnection())
            {
                connection.Execute("Update Players SET FullName = @FullName Set Number= @Number Where Id = @Id;", player);
            }
        }
        public override void Delete(Player player)
        {
            using (var connection = new SqlConnection())
            {
                connection.Execute("Delete From Players Where Id = @Id", player);
            }
        }
    }
}

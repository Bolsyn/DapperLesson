using Dapper;
using DapperLesson.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DapperLesson.Data
{
    public class TeamDataAccess : DataAccess<Team>
    {
        public override ICollection<Team> GetAll()
        {
            using (var connection = new SqlConnection())
            {
                return connection.Query<Team>("Select * from Teams").ToList();
            }
        }
        public override void Insert(Team team)
        {
            using (var connection = new SqlConnection())
            {
                connection.Execute("Insert into Teams values (@Id, @Name)", team);
            }
        }
        public override void Update(Team team)
        {
            using (var connection = new SqlConnection())
            {
                connection.Execute("Update Teams Set Name = @Name Where Id = @Id;", team);
            }
        }
        public override void Delete(Team team)
        {
            using (var connection = new SqlConnection())
            {
                connection.Execute("Delete From Teams Where Id = @Id", team);
            }
        }
    }
}

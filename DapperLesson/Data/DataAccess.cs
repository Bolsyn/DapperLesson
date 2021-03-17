using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DapperLesson.Data
{
    public class DataAccess<T> : IDisposable
    {
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;

        public DataAccess()
        {
            factory = DbProviderFactories.GetFactory("DapperLessonProvider");

            connection = factory.CreateConnection();

            connection.ConnectionString = ConfigurationService.Configuration["DataAccessConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Insert(T entity)
        {
            using (var connection = new SqlConnection())
            {
                if (entity.Equals("FullName")) 
                { 
                    connection.Execute($"Insert into Players values (@Id, @FullName, @Number)", entity);
                }
                if (entity.Equals("Name"))
                {
                    connection.Execute($"Insert into Teams values (@Id, @Name)", entity);
                }
            }
        }

        public void Update(T entity)
        {
            using (var connection = new SqlConnection())
            {
                if (entity.Equals("FullName"))
                {
                    connection.Execute("Update Players SET FullName = @FullName Set Number= @Number Where Id = @Id;", entity);
                }
                if (entity.Equals("Name"))
                {
                    connection.Execute("Update Teams SET Name = @Name Where Id = @Id;", entity);
                }
            }
        }

        public void Delete(T entity)
        {
            using (var connection = new SqlConnection())
            {
                if (entity.Equals("FullName"))
                {
                    connection.Execute("Delete From Players Where Id = @Id", entity);
                }
                if (entity.Equals("Name"))
                {
                    connection.Execute("Delete From Teams Where Id = @Id", entity);
                }
            }
        }

        public ICollection<T> GetAll()
        {
            using (var connection = new SqlConnection())
            {
                return connection.Query<T>("Select * from Teams;Select *from Players").ToList();
            }
        }

        public void ExecuteTranaction(params DbCommand[] commands)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var command in commands)
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}

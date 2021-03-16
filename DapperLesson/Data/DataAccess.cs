using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DapperLesson.Data
{
    public abstract class DataAccess<T> : IDisposable
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

        public abstract void Insert(T entity);

        public abstract void Update(T entity);

        public abstract void Delete(T entity);

        public abstract ICollection<T> GetAll();

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

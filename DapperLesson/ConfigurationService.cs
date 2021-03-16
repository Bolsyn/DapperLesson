using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace DapperLesson
{
    public class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Init()
        {
            if (DbProviderFactories.GetFactory("DapperLessonProvider") == null)
            {
                DbProviderFactories.RegisterFactory("DapperLessonProvider", SqlClientFactory.Instance);
            }

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();                
            }
        }
    }
}

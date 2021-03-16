using System;

namespace DapperLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();


        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}

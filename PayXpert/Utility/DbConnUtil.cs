using Microsoft.Extensions.Configuration;
using PayXpert.Exceptions;


namespace PayXpert.Utility
{
    internal static class DbConnUtil
    {
        private static IConfiguration configuration;

        //Create a Constructor
        static DbConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appSettings.json");
                configuration = builder.Build();
            }
            catch(DatabaseConnectionException Exceptions)
            {
                Console.WriteLine("Couldn't Connect to the DataBase");
            }
        }

        public static string GetConnectionString()
        {
            return configuration.GetConnectionString("LocalConnectionString");
        }
    }
}


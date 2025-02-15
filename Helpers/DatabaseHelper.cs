using System;
using System.IO;
using System.Reflection;

namespace AspNetApi.Helpers
{
    public static class DatabaseHelper
    {
        public static string GetDatabasePath()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string databasePath;

            if (OperatingSystem.IsWindows())
            {
                var appDataPath = Environment.GetEnvironmentVariable("APPDATA");
                databasePath = Path.Combine(appDataPath, appName, "LocalDatabase.db");
            }
            else if (OperatingSystem.IsMacOS())
            {
                var appSupportPath = Path.Combine(Environment.GetEnvironmentVariable("HOME"), "Library", "Application Support");
                databasePath = Path.Combine(appSupportPath, appName, "LocalDatabase.db");
            }
            else if (OperatingSystem.IsLinux())
            {
                var homePath = Environment.GetEnvironmentVariable("HOME");
                databasePath = Path.Combine(homePath, ".local", "share", appName, "LocalDatabase.db");
            }
            else
            {
                databasePath = Path.Combine(Directory.GetCurrentDirectory(), "LocalDatabase.db");
            }

            var directoryPath = Path.GetDirectoryName(databasePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return databasePath;
        }
    }
}

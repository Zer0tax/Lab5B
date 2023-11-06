/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/05/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     4 
 * Purpose: 
 */

using System;
using Microsoft.Extensions.Configuration;

/*
 * dotnet tool install --global dotnet-ef
 * Use nuget package manager to install efcore, efcore.analyzers, design, tools and mysql.data.efcore
 * -- Install-Package Microsoft.EntityFrameworkCore.Tools could do from the console too
 * Get-Help about_EntityFrameworkCore from the console
 *        
 * Scaffold-DbContext "server=127.0.0.1;uid=root;pwd=YOURPASSWORD;database=MMABooks" MySql.Data.EntityFrameworkCore -OutputDir Models -context MMABooksContext -project MMABooksEFClasses -startupproject MMABooksEFClasses -force
 */
namespace MMABooksEFClasses
{
    public class ConfigDB
    {
        public static string GetMySqlConnectionString()
        {
            string folder = System.AppContext.BaseDirectory;
            var builder = new ConfigurationBuilder()
                    .SetBasePath(folder)
                    // Next line points us to the .JSON file with our password.
                    .AddJsonFile("mySqlSettings.json", optional: true, reloadOnChange: true); 

            string connectionString = builder.Build().GetConnectionString("mySql");

            return connectionString;
        }
    }
} // namespace MMABooksEFClasses

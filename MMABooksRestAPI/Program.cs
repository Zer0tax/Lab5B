/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/10/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     5
 * Purpose: Main module
 */

using MMABooksEFClasses.Models;

namespace MMABooksRestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // add corspolicy - in a production app lock this down!
            builder.Services.AddCors(options => {
                options.AddDefaultPolicy(
                builder => {
                    builder.AllowAnyOrigin()
                    .WithMethods("post", "put", "delete", "get", "options")
                    .AllowAnyHeader();
                });
            });

            // adding the dbcontextto the service
            builder.Services.AddDbContext<MMABooksContext>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            // Enables the cors policy
            // CORS is supposed to protect your personal data. 
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    } // end class Program
} // end namespace MMABooksRestAPI
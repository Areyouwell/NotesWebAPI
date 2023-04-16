using Microsoft.Extensions.DependencyInjection;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;
using System.Reflection;
using Notes.WebApi.Middleware;

namespace Notes.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.AddControllers();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<NotesDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {

                }
            }

            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.MapControllers();
            app.Run();
        }
    }
}
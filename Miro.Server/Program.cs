using Microsoft.EntityFrameworkCore;

using Miro.Server.Entities;
using Miro.Server.Interfaces;
using Miro.Server.Services;

namespace Miro.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddScoped<IAccountManager, AccountManager>();
            builder.Services.AddScoped<IRepository<User>, UserRepository<User>>();
            builder.Services.AddScoped<IResultModel<User>, ResultModel<User>>();
            builder.Services.AddScoped<User>();
            builder.Services.AddSignalR();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure DbContext
            builder.Services.AddDbContext<DBContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("Connect2")));

            builder.Services.AddScoped<ITokenService<User>>(provider =>
                  new TokenService<User>(
                      builder.Configuration["JwtSettings:SecretKey"],
                      provider.GetRequiredService<DBContext>(),
                      provider.GetRequiredService<IRepository<User>>()
                ));

            var app = builder.Build();

            // Apply pending migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
                dbContext.Database.Migrate();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MiroHub>("/MiroHub");
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
using Infrastructure;
using IntegrationTest.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class CustomWebApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram : class
    {
        private SqliteConnection _connection;
        public CustomWebApplicationFactory()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.SingleOrDefault(d=> d.ServiceType == typeof(DbContextOptions<DataContext>));
                services.Remove(serviceDescriptor);
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlite().BuildServiceProvider();
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlite(_connection);
                    options.UseInternalServiceProvider(serviceProvider);
                },ServiceLifetime.Scoped);
                var sp = services.BuildServiceProvider();
                using(var scope = sp.CreateScope())
                {
                    var scopedService = scope.ServiceProvider;
                    var db = scopedService.GetRequiredService<DataContext>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    Utilities.InitializeDbForTest(db);
                }
            });
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
            _connection.Dispose();
        }
    }
}

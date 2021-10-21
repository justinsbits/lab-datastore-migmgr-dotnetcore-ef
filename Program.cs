using CommanderDA;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CommanderDBMigrationMgr
{
    class Program
    {
        // https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host
        public static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            host.Run();
        }

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        builder.AddUserSecrets<Program>();
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var sqlConStrBuilder = new SqlConnectionStringBuilder(
                    hostContext.Configuration.GetConnectionString("CommandConStr"));
                    sqlConStrBuilder.Password = hostContext.Configuration["DbPassword"];
                    string sqlConStr = sqlConStrBuilder.ConnectionString;

                    services
                        .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConStr, b => b.MigrationsAssembly("CommanderDBMigrationMgr")));
                });
        }

        // Example of working with custom config
        //
        //public static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
        //    String sqlServerConStr = null;
        //    hostBuilder = hostBuilder.ConfigureHostConfiguration(configHost =>
        //        {
        //            IConfiguration config = configHost.SetBasePath(Directory.GetCurrentDirectory())
        //               .AddJsonFile("commanderDBSettings.json", optional: false, reloadOnChange: true)
        //               .AddEnvironmentVariables()
        //               .AddCommandLine(args).Build();
        //            sqlServerConStr = config.GetValue<string>("ConnectionStrings:CommandConStr");
        //        });

        //    hostBuilder.ConfigureServices((hostContext, services) =>
        //    {
        //        services
        //            .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlServerConStr, b => b.MigrationsAssembly("CommanderDBMigrationMgr")));
        //    });

        //    return hostBuilder;
        //}


        // Example using direct DbContext creation (but ef tools then not usable since it relies on finding and applying ".Build" against a method that addresses "CreateHostBuilder")
        //
        //static void Main(string[] args)
        //{
        //    var builder = new ConfigurationBuilder()
        //       .SetBasePath(Directory.GetCurrentDirectory())
        //       .AddJsonFile("commanderDBSettings.json", optional: false, reloadOnChange: true)
        //       .AddEnvironmentVariables()
        //       .AddCommandLine(args);
        //    IConfiguration config = builder.Build();
        //    var sqlServerConStr = config.GetValue<string>("ConnectionStrings:CommandConStr");
        //    var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(sqlServerConStr).Options;
        //    using (var ctx2 = new AppDbContext(options))
        //    {
        //        ctx2.Database.Migrate();
        //    }
        //}


        // Playing with ServiceCollection / Provider here (but then you lost benefit of the hosted object that is a one-stop-shop for obj liftime mgmt
        // 
        //static readonly ServiceCollection Services = new ServiceCollection();
        //static void Main(string[] args)
        //{
        //    var builder = new ConfigurationBuilder()
        //       .SetBasePath(Directory.GetCurrentDirectory())
        //       .AddJsonFile("commanderDBSettings.json", optional: false, reloadOnChange: true)
        //       .AddEnvironmentVariables()
        //       .AddCommandLine(args);
        //    IConfiguration config = builder.Build();

        //    var sqlServerConStr = config.GetValue<string>("ConnectionStrings:CommandConStr");
        //    Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlServerConStr)
        //     .LogTo(
        //         Console.WriteLine, // ...or log to file etc.
        //         new[] { DbLoggerCategory.Database.Command.Name,
        //                            DbLoggerCategory.Database.Transaction.Name },
        //         LogLevel.Debug));
        //    ServiceProvider serviceProvider = Services.BuildServiceProvider();
        //    var ctx = serviceProvider.GetService<AppDbContext>();
        //    ctx.Database.EnsureCreated();
        //    ctx.Database.Migrate();

        //}
    }

}

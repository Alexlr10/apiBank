using System.Diagnostics.CodeAnalysis;
using apiBank.src.Api;
using apiBank.src.BusinessRules.Handlers;
using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Validators;
using apiBank.src.BusinessRules.Validators.Interfaces;
using apiBank.src.Database;
using apiBank.src.Database.Repositories;
using apiBank.src.Database.Repositories.Interfaces;

namespace apiBank
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddDbContext<BankContext>();

                        services.AddGraphQLServer()
                            .AddQueryType<Query>()
                            .AddMutationType<Mutation>();

                        services.AddScoped<IContaCorrenteValidator, ContaCorrenteValidator>();
                        services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
                        services.AddScoped<IUpsertCCHandler, UpsertCCHandler>();
                        services.AddScoped<IGetAllCCHandler, GetAllCCHandler>();
                        services.AddScoped<IGetByIdCCHandler, GetByIdCCHandler>();
                        services.AddScoped<IGetByContaVerSaldoHandler, GetByContaVerSaldoHandler>();
                        services.AddScoped<ISacarContaHandler, SacarContaHandler>();
                        services.AddScoped<IDepositarContaHandler, DepositarContaHandler>();

                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                    });

                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGraphQL();
                        });
                    });
                });
    }
}

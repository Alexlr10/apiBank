using apiBank.src.Api;
using apiBank.src.BusinessRules.Handlers;
using apiBank.src.BusinessRules.Handlers.Interfaces;
using apiBank.src.BusinessRules.Validators;
using apiBank.src.BusinessRules.Validators.Interfaces;
using apiBank.src.Database;
using apiBank.src.Database.Repositories;
using apiBank.src.Database.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adicione o contexto do banco de dados como servico
builder.Services.AddDbContext<BankContext>();

// Add services to the container.
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

// Validators
builder.Services.AddScoped<IContaCorrenteValidator, ContaCorrenteValidator>();

// Repositories
builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();

// Business rules
builder.Services.AddScoped<IUpsertCCHandler, UpsertCCHandler>();
builder.Services.AddScoped<IGetAllCCHandler, GetAllCCHandler>();
builder.Services.AddScoped<IGetByIdCCHandler, GetByIdCCHandler>();
builder.Services.AddScoped<IGetByContaVerSaldoHandler, GetByContaVerSaldoHandler>();
builder.Services.AddScoped<ISacarContaHandler, SacarContaHandler>();
builder.Services.AddScoped<IDepositarContaHandler, DepositarContaHandler>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGraphQL();

app.Run();

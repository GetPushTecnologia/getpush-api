using GetPush_Api.Domain.Commands.Handlers;
using GetPush_Api.Domain.Commands.Results.map;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Infra.Repositories;
using GetPush_Api.Shared;
using Serilog;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Read configuration from appsettings.json
var configuration = builder.Configuration;

// Configure services
//CommandHandler
builder.Services.AddTransient<AccountCommandHandler>();

//Repository
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

//Map
builder.Services.AddTransient<UsuarioMap, UsuarioMap>();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (Exception ex)
    {
        // Log error details
        app.Logger.LogError(ex, "An error occurred while processing the request.");

        // Send a generic error response
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
    }
});

app.MapControllers();

// Set the connection string
Runtime.ConnectionString = configuration.GetConnectionString("CnnStr")
                           ?? throw new InvalidOperationException("Connection string 'CnnStr' not found.");
Runtime.ClientManagers = configuration.GetConnectionString("ClientManagers")
                           ?? throw new InvalidOperationException("Connection string 'CnnStr' not found.");

app.Run();

using GetPush_Api.Domain.Commands.Handlers;
using GetPush_Api.Domain.Commands.Results.map;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Infra.Repositories;
using GetPush_Api.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Read configuration from appsettings.json
var configuration = builder.Configuration;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Configure services
ConfigureServices(builder.Services, configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
Configure(app, app.Environment);

app.Run();

// Configuration methods
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Add your services here
    services.AddTransient<AccountCommandHandler>();
    services.AddTransient<IAccountRepository, AccountRepository>();
    services.AddTransient<UsuarioMap, UsuarioMap>();

    // Set the connection string
    Runtime.ConnectionString = configuration.GetConnectionString("CnnStr")
                               ?? throw new InvalidOperationException("Connection string 'CnnStr' not found.");
    Runtime.ClientManagers = configuration.GetConnectionString("ClientManagers")
                               ?? throw new InvalidOperationException("Connection string 'ClientManagers' not found.");
    Runtime.KeySecurityToken = configuration.GetConnectionString("KeySecurityToken")
                               ?? throw new InvalidOperationException("Connection string 'KeySecurityToken' not found.");

    var symmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Runtime.KeySecurityToken));

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "seu-issuer",
            ValidAudience = "seu-audience",
            IssuerSigningKey = symmetricKey
        };
    }).AddCookie(options =>
    {
        options.Cookie.Name = "YourSessionCookieName"; // Nome do cookie de sessão
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Altere conforme necessário
        options.Cookie.SameSite = SameSiteMode.Strict; // Melhorar a segurança, conforme necessário
        options.Cookie.IsEssential = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
        options.SlidingExpiration = true; // Renova a expiração em cada requisição
    });

    builder.Services.AddAuthentication();

    // Clear default logging providers and add Serilog
    services.AddLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSerilog(Log.Logger); // Utiliza Serilog para logging
    });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
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
    app.UseRouting();

    // Use authentication and authorization
    app.UseAuthentication();
    app.UseAuthorization();

    app.Use(async (context, next) =>
    {
        try
        {
            await next.Invoke();
        }
        catch (Exception ex)
        {
            // Log error details using Serilog
            //Log.Logger.LogError(ex, "An error occurred while processing the request.");

            // Send a generic error response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
        }
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
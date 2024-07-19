using GetPush_Api.Domain.Commands.Handlers;
using GetPush_Api.Domain.Commands.Results.map;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Domain.Services;
using GetPush_Api.Domain.Util;
using GetPush_Api.Infra.Repositories;
using GetPush_Api.Infra.Services;
using GetPush_Api.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("F", "false");

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
ConfigureServices(builder.Services, configuration, builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
Configure(app, app.Environment);

app.Run();

// Configuration methods
void ConfigureServices(IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
{
    services.AddControllers()
         .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserva o caso das propriedades
             options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
         });

    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

        // Define o esquema de segurança
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insira 'Bearer' [espaço] e seu token JWT",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

    // Command Handler
    services.AddTransient<AccountCommandHandler>();
    services.AddTransient<ContasPagasCommandHandler>();
    services.AddTransient<TipoContasPagasCommandHandler>();
    services.AddTransient<ValorRecebidoCommandHandler>();
    services.AddTransient<TipoValorRecebidoCommandHandler>();

    // Repository
    services.AddTransient<IAccountRepository, AccountRepository>();
    services.AddTransient<IContasPagasRepository, ContasPagasRepository>();
    services.AddTransient<ITipoContaPagaRepository, TipoContaPagaRepository>();
    services.AddTransient<IValorRecebidoRepository, ValorRecebidoRepository>();
    services.AddTransient<ITipoValorRecebidoRepository, TipoValorRecebidoRepository>();
    // Map
    services.AddTransient<UsuarioMap>();

    // Utils
    services.AddTransient<Utilidades>();

    // Set the connection string
    Runtime.ConnectionString = configuration.GetConnectionString("CnnStr")
                               ?? throw new InvalidOperationException("Connection string 'CnnStr' not found.");
    Runtime.ClientManagers = configuration.GetConnectionString("ClientManagers")
                               ?? throw new InvalidOperationException("Connection string 'ClientManagers' not found.");
    Runtime.KeySecurityToken = configuration.GetConnectionString("KeySecurityToken")
                               ?? throw new InvalidOperationException("Connection string 'KeySecurityToken' not found.");


    builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("CnnStr")));

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

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "api1");
        });
    });

    // Clear default logging providers and add Serilog
    services.AddLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSerilog(Log.Logger); // Utiliza Serilog para logging
    });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //if (env.IsDevelopment())
    //{
    //    app.UseDeveloperExceptionPage();
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}
    //else
    //{
    //    app.UseExceptionHandler("/Home/Error");
    //    app.UseHsts();
    //}

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

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
            Log.Logger.Error(ex, "An error occurred while processing the request.");

            // Send a detailed error response
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = "An unexpected fault happened. Try again later.",
                Error = ex.Message,
                StackTrace = ex.StackTrace
            };

            var errorJson = System.Text.Json.JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(errorJson);
        }
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}

using Microservice.Authentication.Data;
using Microservice.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Configuration des logs
var configuration = builder.Configuration;
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .WriteTo.Console()
    .CreateLogger();

//base de données
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("UserConnection")));

//var connectionString = builder.Configuration.GetConnectionString("UserConnection");
//builder.Services.AddDbContext<UserDbContext>(options =>
//options.UseSqlServer(connectionString));


// Identity

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options=>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
}
)
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();



 // Configuration pour l'authentification

//builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateAudience = true,
        //ValidAudience = configuration[key: "JWT:ValidAudience"],
        //ValidIssuer = configuration[key: "JWT:ValidIssuer"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        ValidAudience = configuration["JwtConfig:ValidAudience"] ?? throw new ArgumentNullException("JwtConfig:ValidAudience"),
        ValidIssuer = configuration["JwtConfig:ValidIssuer"] ?? throw new ArgumentNullException("JwtConfig:ValidIssuer"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"] ?? throw new ArgumentNullException("JwtConfig:Secret")))

    };
});


// Configuration des politiques d'autorisation
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RHPolicy", policy => policy.RequireRole("RH"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

// Configuration des contrôleurs
builder.Services.AddControllers();


// Configuration de Swagger/OpenAPI

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Veuillez saisir un jeton valide",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

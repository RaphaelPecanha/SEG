using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SEG.Context;
using SEG.Extension;
using SEG.Filters;
using SEG.Logging;
using SEG.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<AppDbContext>().
    AddDefaultTokenProviders();

#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
builder.Services.AddDbContext<AppDbContext>(options =>
                                  options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

var secretKey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("Invalid secrety Key");

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    //Quando receber o token aqui será validado
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure HTTPS Redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 44342; // Porta HTTPS configurada
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //Melhorando o trace error
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

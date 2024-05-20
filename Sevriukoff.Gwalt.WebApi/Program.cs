using System.Text;
using System.Text.Json.Serialization;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Mapping;
using Sevriukoff.Gwalt.Application.Services;
using Sevriukoff.Gwalt.Infrastructure;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Repositories;
using Sevriukoff.Gwalt.WebApi.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtBearerConfig"));
builder.Services.Configure<YandexStorageConfig>(builder.Configuration.GetSection("YandexStorageConfig"));

#region Authentication

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JwtBearerConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerConfig:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerConfig:SecretKey"]))
    };

    opt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("jwt-access", out var accessToken))
            {
                //var jwtHelper = context.HttpContext.RequestServices.GetRequiredService<JwtHelper>();
                
                context.Token = accessToken;
            }
            
            return Task.CompletedTask;
        }
    };
});

#endregion

#region Authorization

builder.Services.AddAuthorization(opt =>
{

});

#endregion

#region DbContext

builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

#endregion

#region DependencyInjections

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    opt.InstanceName = "Gwalt";
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<PasswordHasher>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAmazonS3, AmazonS3Client>(provider =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = builder.Configuration["YandexStorageConfig:ServiceURL"],
    };
    
    return new AmazonS3Client(config);
});

builder.Services.AddScoped<IFileStorage, YandexStorage>();
builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile), typeof(PresentationMappingProfile));

#endregion

builder.Services.AddControllers()
var provider = builder.Services.BuildServiceProvider();

builder.Services.AddControllers(opt =>
    {
        opt.ValueProviderFactories.Add(new JwtValueProviderFactory(provider.GetRequiredService<JwtHelper>()));
        opt.ValueProviderFactories.Add(new CookieValueProviderFactory());
    })
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
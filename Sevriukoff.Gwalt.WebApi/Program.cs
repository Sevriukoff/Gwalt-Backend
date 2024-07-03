using System.Text;
using System.Text.Json.Serialization;
using Amazon.S3;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Npgsql;
using Sevriukoff.Gwalt.Application.Handlers;
using Sevriukoff.Gwalt.Application.Helpers;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Mapping;
using Sevriukoff.Gwalt.Application.Services;
using Sevriukoff.Gwalt.Infrastructure;
using Sevriukoff.Gwalt.Infrastructure.Base;
using Sevriukoff.Gwalt.Infrastructure.Caching;
using Sevriukoff.Gwalt.Infrastructure.Entities;
using Sevriukoff.Gwalt.Infrastructure.External;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Repositories;
using Sevriukoff.Gwalt.WebApi.Common;
using Sevriukoff.Gwalt.WebApi.Common.Attributes;
using Sevriukoff.Gwalt.WebApi.Mapping;
using Sevriukoff.Gwalt.WebApi.Middleware;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var r = builder.Host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
{
    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<TrackRepository>().As<ITrackRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<TrackService>().As<ITrackService>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<AlbumRepository>().As<IAlbumRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AlbumService>().As<IAlbumService>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<LikeRepository>().As<ILikeRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<LikeService>().As<ILikeService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<TrackLikeHandler>().As<ILikeHandler>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AlbumLikeHandler>().As<ILikeHandler>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<CommentLikeHandler>().As<ILikeHandler>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<ListenRepository>().As<IListenRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ListenService>().As<IListenService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<TrackListenHandler>().As<IListenHandler>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AlbumListenHandler>().As<IListenHandler>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<SearchService>().As<ISearchService>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<JwtHelper>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<PasswordHasher>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<SessionService>().As<ISessionService>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope();
    
    containerBuilder.RegisterType<RedisCacheUpdater>().As<ICacheUpdater>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<BaseCacheApplier<Track>>().As<ICacheApplier<Track>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<BaseCacheApplier<Album>>().As<ICacheApplier<Album>>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserCacheApplier>().As<ICacheApplier<User>>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<TrackRepository>().As<IRepository<Track>>();
    containerBuilder.RegisterType<AlbumRepository>().As<IRepository<Album>>();
    containerBuilder.RegisterType<UserRepository>().As<IRepository<User>>();
    
    containerBuilder.RegisterDecorator<BaseRepositoryWithCache<Track>, IRepository<Track>>();
    containerBuilder.RegisterDecorator<BaseRepositoryWithCache<Album>, IRepository<Album>>();
    containerBuilder.RegisterDecorator<BaseRepositoryWithCache<User>, IRepository<User>>();
    containerBuilder.RegisterDecorator<TrackRepositoryWithCache, ITrackRepository>();
    containerBuilder.RegisterDecorator<AlbumRepositoryWithCache, IAlbumRepository>();
    containerBuilder.RegisterDecorator<UserRepositoryWithCache, IUserRepository>();
    
    containerBuilder.RegisterType<ListenCacheClient>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<FollowerCacheClient>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<LikeCacheClient>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<AudioProcessor>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<DateTimeHelper>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<ImageProcessor>().InstancePerLifetimeScope();

    containerBuilder.Register<IAmazonS3>(context =>
    {
        var config = new AmazonS3Config
        {
            ServiceURL = builder.Configuration["YandexStorageConfig:ServiceURL"],
        };
        
        return new AmazonS3Client(config);
    }).InstancePerLifetimeScope();

    containerBuilder.RegisterType<YandexStorage>().As<IFileStorage>().InstancePerLifetimeScope();
});

builder.Services.AddHostedService<CacheUpdateBackgroundService>();
builder.Services.AddScoped<JwtHelper>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<YandexStorageConfig>(builder.Configuration.GetSection("YandexStorageConfig"));
builder.Services.Configure<CookieConfig>(builder.Configuration.GetSection("CookieConfig"));
builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));

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
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]))
    };

    opt.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.TryGetValue("access-token", out var accessToken))
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

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Postgres"));
dataSourceBuilder.MapEnum<Gender>();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseNpgsql(dataSource);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#endregion

#region DependencyInjections

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    opt.InstanceName = "Gwalt";
});

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
builder.Services.AddScoped<ICacheProvider>(provider => 
{
    var redisConnection = provider.GetRequiredService<IConnectionMultiplexer>();
    return new RedisCacheProvider(redisConnection);
});

builder.Services.AddScoped<ICacheKeyManager>(_ => 
{
    var instanceName = "Gwalt";
    return new RedisCacheKeyManager(instanceName);
});

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile), typeof(PresentationMappingProfile));

#endregion

var provider = builder.Services.BuildServiceProvider();

builder.Services.AddControllers(opt =>
    {
        opt.ValueProviderFactories.Add(new JwtValueProviderFactory(provider.GetRequiredService<JwtHelper>()));
        opt.ValueProviderFactories.Add(new CookieValueProviderFactory());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    })
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "http://localhost:62295")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithExposedHeaders("Location");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost3000");

app.UseAuthentication();
app.UseAuthorization();

app.UseDelayMiddleware(1500);
app.UsePublicSessionMiddleware();

app.MapControllers();

app.Run();
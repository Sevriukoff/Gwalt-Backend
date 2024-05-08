using System.Text.Json.Serialization;
using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using Sevriukoff.Gwalt.Application.Interfaces;
using Sevriukoff.Gwalt.Application.Services;
using Sevriukoff.Gwalt.Infrastructure;
using Sevriukoff.Gwalt.Infrastructure.Interfaces;
using Sevriukoff.Gwalt.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//TODO: Add Autofac

builder.Services.AddDbContext<DataDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITrackRepository, TrackRepository>();
builder.Services.AddScoped<ITrackService, TrackService>();

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

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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

app.UseAuthorization();

app.MapControllers();

app.Run();
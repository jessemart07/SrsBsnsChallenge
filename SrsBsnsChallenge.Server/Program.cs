using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using SrsBsnsChallenge.Server;
using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:4200");
        });
});

// Add controllers
builder.Services.AddControllers();

// Inject dbContext
builder.Services.AddDbContext<SrsBsnsChallengeDbContext>(dbContextOptionsBuilder =>
    dbContextOptionsBuilder.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"]));
// Configure Email Service
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
// Add dependency injection
builder.Services
    .AddScoped<IContactFormService, ContactFormService>()
    .AddTransient<IEmailService, EmailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "SrsBsns Challenge Web API", Version = "v1" });
});

var app = builder.Build();

app.UseCors("CORSPolicy");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.MapContactFormsEndpoints();
app.MapControllers();

app.Run();

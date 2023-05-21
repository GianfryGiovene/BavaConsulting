using CleanArchitecture.API.Controllers;
using CleanArchitecture.ApplicationCore;
using CleanArchitecture.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var MyAllowAllOrigins = "_myAllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
IdentityModelEventSource.ShowPII = true;
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationCore();
builder.Services.AddAutoMapper(typeof(Program));




builder.Services.AddControllers().AddNewtonsoftJson(options=> 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore

);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrigins,
        policy =>
        {
            policy.SetIsOriginAllowed((host) => true)
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader();                
        });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization che usa il modello Bearer (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//per utilizzare un controller di gestione eccezioni
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        await ExceptionController.HandleExceptions(context);
    });
});

app.UseHttpsRedirection();

app.UseCors("_myAllowAllOrigins");

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

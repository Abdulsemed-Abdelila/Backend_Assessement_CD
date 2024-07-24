using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using Application;
using Infrastructure;
using Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddSwaggerDoc(builder.Services);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersitenceServices(builder.Configuration);
builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

// Add Environment Variables
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim(JwtRegisteredClaimNames.Typ, "startup", "investor"));
});

//Date now works with this for east african time
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


//builder.Services.AddApplication().
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// run the app
// app.UseCors("frontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

// Add swagger documentation
void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Spark Tank",
        });
    });
}

public partial class Program { }
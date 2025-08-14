using api.Data;
using api.Interfaces;
using api.Models;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // Add security scheme
        document.Components ??= new();
        document.Components.SecuritySchemes["Bearer"] = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your valid JWT."
        };

        // Apply scheme globally
        document.SecurityRequirements.Add(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        return Task.CompletedTask;
    });
}); //dependency injection and services

builder.Services.AddControllers();
builder.Services.AddControllers()
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    {
        
    }
});
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDBContext>(); //? 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"], // the issuer is the server, which address is it coming from? the audience is the same thing
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"], // just reading the builder configuration json and collecting da data from it
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
    
}); // this is where we add the token validation parameters
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<ITokenService, TokenService>(); // another dependency injection, now we can give out everything here.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //this is where all the middleware is
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo APi");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization(); //middleware


app.MapControllers();
app.Run(); //actually runs the program


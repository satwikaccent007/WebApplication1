using Asp.Versioning;
using Asp.Versioning.Conventions;
using DataLayer.Irepo;
using DataLayer.Models;
using DataLayer.Repos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDb>(options =>

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<Iuser,UserRepo>();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true; // if no version is specied the application will exe default method 
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0); // if verison id specid this will execute
    options.ReportApiVersions = true;  // responce header inluces supported version
}).AddMvc(o =>
{
    o.Conventions.Add(new VersionByNamespaceConvention()); //version controller based on their namespace
}).AddApiExplorer(x =>
{
    x.GroupNameFormat = "'v'V";
    x.SubstituteApiVersionInUrl = true; // this helpful for swagger
});
builder.Services.AddScoped<JwtService>();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"]
             ?? throw new InvalidOperationException("JWT Key missing.");

//This configures the authentication 
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) //tells that we are using JWT authentication so auth will look for jwt token with req
    .AddJwtBearer(options => // this tells what validations to perform on JWT token 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuerSigningKey = true, // validate the signature using the JWT key

            IssuerSigningKey = new SymmetricSecurityKey(  //convert the key to BYTE
                Encoding.UTF8.GetBytes(jwtKey)),

            ValidateIssuer = true, //validate the issuer
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            ValidateAudience = true, //validate the audience
            ValidAudience = builder.Configuration["Jwt:Audience"],
                                                        
            ValidateLifetime = true, //check if token is expired or not

            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

// configuring the cors policy
// cors i.e. cross origin resource sharing is a browser security mechanism
// by default browser restrcites to access/req resource from different origin i.e. domain/port etc 
// now using cors policy server can mention which services can access resouces from it 
// rest services are not allowed
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
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
app.UseCors("ReactPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto3.Dados;
using Projeto3.Dados.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//JWTConfig 
var key = Encoding.ASCII.GetBytes(builder.Configuration["Key:JwtKey"].ToString());

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience         = true,
        IssuerSigningKey         = new SymmetricSecurityKey(key),
        ValidateIssuer           = true,
        ValidIssuer              = "Waldir Lima Editora Ltda",
        ValidAudience            = "https://localhost:7273"
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Projeto3 - Acesso ao banco de dados", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name         = "Authorization",
        Type         = SecuritySchemeType.ApiKey,
        Scheme       = "Bearer",
        BearerFormat = "JWT",
        In           = ParameterLocation.Header,
        Description  = "JWT Authorization header using the Bearer scheme",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
       {
          new OpenApiSecurityScheme
          {
             Reference = new OpenApiReference
             {
               Type = ReferenceType.SecurityScheme,
               Id   = "Bearer"
             }
          },
          new string[] {}
       }
    });
});

//inserindo controle de autorização na aplicação 
builder.Services.AddAuthorization();


//injeção de dependência
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IClientePersistence, ClientePersistence>();

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

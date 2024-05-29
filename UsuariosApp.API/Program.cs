using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UsuariosApp.API.Security;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;
using UsuariosApp.Infra.Messages.Consumers;
using UsuariosApp.Infra.Messages.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(config => { config.LowercaseUrls = true; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UsuariosApp - API para controle de usuários e permissões",
        Description = "Treinamento C# WebDeveloper - COTI Informática",
        Version = "1.0",
        Contact = new OpenApiContact
        {
            Name = "COTI Informática",
            Email = "contato@cotiinformatica.com.br",
            Url = new Uri("http://www.cotiinformatica.com.br")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Entre com o TOKEN JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[]{ }
                        }
                    });

});

#region Adicionando as injeções de dependência do projeto

builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IPerfilRepository, PerfilRepository>();
builder.Services.AddTransient<IMessageProducer, MessageProducer>();

#endregion

#region Adicionando a política de autenticação do projeto

builder.Services.AddAuthentication(auth =>
{
    //definindo o tipo de autenticação da API como JWT - JSON WEB TOKENS
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => 
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        //chave secreta para validar os tokens da API
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(JwtTokenSecurity.SecurityKey))
    };
});

#endregion

#region Registrando a classe consumer

builder.Services.AddHostedService<MessageConsumer>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//tornando a classe Program pública
public partial class Program { }

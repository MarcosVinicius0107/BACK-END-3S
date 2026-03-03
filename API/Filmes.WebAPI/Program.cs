using Filmes.WebAPI.BdcontextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Repositories;
using FilmesTorloni.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o contexto do banco de dados (exemplo com SQL Server)
builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Adiciona o repositório ao container de injeção de dependência
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//Adiciona o serviço de autenticação JWT Bearer(forma de autenticação)
object value = builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //valida quem está solicitando
            ValidateIssuer = true,

            //valida quem está Recebendo
            ValidateAudience = true,

            //define se o tempo de expiração será autenticação
            ValidateLifetime = true,

            //forma de criptografia e valida a chave de autenticação
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),

            //valida o tempo de expiração do token
            ClockSkew = TimeSpan.FromMinutes(5),

            //nome do issuer (de onde está vindo)
            ValidIssuer = "Filmes.WebAPI",

            //nome do audience (para onde ele está indo)
            ValidAudience = "Filmes.WebAPI"

        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Options =>
{
    Uri uri = new Uri("https://opensource.org/licenses/MIT");
    Options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "Uma API com catálogo de filmes",
        TermsOfService = new Uri("http://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "MarcosVinicius0107",
            Url = new Uri("https://github.com/MarcosVinicius0107")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com./license")
        }
    });

    Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"
    });

    Options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });

});

//Adiciona o serviço de Controllers
builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();

app.UseAuthorization();

//Adiciona o mapeamento de Controllers
app.MapControllers();

app.Run();

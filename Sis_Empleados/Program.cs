using Infraestructura.ConfiguracionApp;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(option =>
    {
        option.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

//instacias de aplicacion
var configServicios = new AppConfigServicios();
configServicios.ConfigurarServiciosContainer(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();

app.Use(async (context, next) =>
{   
    if(context.Request.Path == "/api/login")
    {
        await next.Invoke();
        return;
    }

    var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
    if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    {
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>();
        var parametrosValidacionToken = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtConfig.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
            ValidateLifetime = true
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, parametrosValidacionToken, out _);

            await next.Invoke();
            return;
        }
        catch (SecurityTokenException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token no valido.");
            return;
        }
    }

    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    await context.Response.WriteAsync("Token de autorización no encontrado.");
});


app.UseAuthorization();


app.MapControllers();

app.Run();

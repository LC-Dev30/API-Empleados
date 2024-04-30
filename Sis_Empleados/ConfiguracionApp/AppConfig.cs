using Aplicacion.Servicios;
using Aplicacion.Servicios.ServiciosLocker;
using Arquitectura.Persistencia.Repositorios;
using Domain.Entidades.Empleados.Interfaces;
using Domain.Entidades.Lockers.Interfaces;
using Domain.Servicios.EmpleadosServicios;
using Domain.Servicios.LockersServicios;
using Infraestructura.Persistencia.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Infraestructura.ConfiguracionApp
{
    public class AppConfig
    {
        public void ConfigurarServiciosContainer(IServiceCollection services, IConfiguration config)
        {
            //Aplicacion
            services.AddScoped<IServiceEmpleadoAplicacion,EmpleadoServicioAplicacion>();
            services.AddScoped<IServicioLockerAplicacion,ServicioLockerAplicacion>();

            //Domain
            services.AddScoped<IEmpleadoServicioDomain,EmpleadoServicioDomain>();
            services.AddScoped<IServicioLockerDomain,ServicioLockerDomain>();

            //Infraestructura
            services.AddScoped<IRepositorioEmpleadoDomain, EmpleadoRepositorio>();
            services.AddScoped<IRepositorioLockerDomain, LockerRepositorio>();

            //configuracion JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                var options = option.TokenValidationParameters;

                options.ValidateIssuer = true;
                options.ValidateAudience = true;
                options.ValidateLifetime = true;
                options.ValidateIssuerSigningKey = true;
                options.ValidIssuer = config["Jwt:Issuer"];
                options.ValidAudience = config["Jwt:Audience"];
                options.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            });
        }
    }
}

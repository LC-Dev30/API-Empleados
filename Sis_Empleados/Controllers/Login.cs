using Domain.Entidades.Admin;
using Infraestructura.ConfiguracionApp;
using Infraestructura.Servicios.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Login : ControllerBase
    {
        IConfiguration _configuration;
        ILoginServicio _loginServicio;

        public Login(IConfiguration configuration, ILoginServicio loginServicio)
        {
           _configuration = configuration;
           _loginServicio = loginServicio;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginService(Admin admin)
        {
            var resServicio = await _loginServicio.LoginService(admin);

            if (resServicio.StatusCode != 200 || resServicio.Data == null)
                return Problem(statusCode:resServicio.StatusCode, detail:resServicio.Message);

            var adminPalse = (Admin)resServicio.Data;
            var token = AutenticacionService.CrearToken(adminPalse,_configuration);
            return Ok(token);
        }

        [HttpGet("prueba")]
        [Authorize]
        public  IActionResult Prueba()
        {     
            return Ok("Lista de pruebas");
        }
    }
}

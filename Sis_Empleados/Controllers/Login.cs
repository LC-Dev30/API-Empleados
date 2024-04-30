using Infraestructura.ConfiguracionApp;
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

        public Login(IConfiguration configuration)
        {
           _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult LoginService(Admin admin)
        {
            if (admin == null)
                return BadRequest("Campos vacios o nulos");

            var loginService = new AutenticacionService(_configuration);
            var token = loginService.CrearToken(admin);
            return Ok(token);
        }
    }
}

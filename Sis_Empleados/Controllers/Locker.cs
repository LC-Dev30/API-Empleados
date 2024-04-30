using Aplicacion.Servicios.ServiciosLocker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infraestructura.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Locker : ControllerBase
    {
        private IServicioLockerAplicacion _servicioLocker;

        public Locker(IServicioLockerAplicacion servicioLocker)
        {
          _servicioLocker = servicioLocker;
        }

        [HttpGet("lockers")]
        public async Task<IActionResult> ListaLocker()
        {
            var lockers = await _servicioLocker.ListaLockersServicio();
            return Ok(lockers);
        }

        [HttpPost("locker/{numLockers}")]
        public async Task<IActionResult> AgregarLocker([FromRoute] int numLockers)
        {
           var res = await _servicioLocker.AgregarLockerServicio(numLockers);
           return Ok(res);
        }

        [HttpGet("locker")]
        public async Task<IActionResult> AsignarLocker(int nLocker)
        {
           var lockerResponse = await _servicioLocker.AsignarLockerServicio(nLocker);
            return Ok(lockerResponse);
        }
    }
}

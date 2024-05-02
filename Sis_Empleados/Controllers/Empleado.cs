using Aplicacion.Servicios;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arquitectura.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class Empleado : ControllerBase
    {
        private readonly IServiceEmpleadoAplicacion _servicioEmpleados;

        public Empleado(IServiceEmpleadoAplicacion servicioEmpleados)
        {
          _servicioEmpleados = servicioEmpleados;
        }

        [HttpGet("empleados")]
        public async Task<IActionResult> ListaEmpleados()
        {
            var data = await _servicioEmpleados.GetEmpleadosServicios();
            return Ok(data);
        }

        [HttpPost("empleado")]
        public async Task<IActionResult> AgregaEmpleado(EmpleadoDTO empleado)
        {
            var res = await _servicioEmpleados.AgregaEmpleadoServicio(empleado);
            return Ok(res);
        }

        [HttpPut("empleado")]
        public async Task<IActionResult> EditarEmpleado(EmpleadoDTO empleado)
        {
           var res = await _servicioEmpleados.EditarEmpleadoServicio(empleado);
            return Ok(res);
        }

        [HttpDelete("empleado/{codigoEmpleado}/{lockerAsignado}")]
        public async Task<IActionResult> EliminarEmpleado(int codigoEmpleado,int lockerAsignado)
        {
          var res = await _servicioEmpleados.EliminarEmpleadServicio(codigoEmpleado,lockerAsignado); 
            return Ok(res);
        }

        [HttpGet("empleado/{codigoOrNombreEmpleado}")]
        public async Task<IActionResult> EmpleadoPorCodigo(string codigoOrNombreEmpleado)
        {
            var data = await _servicioEmpleados.EmpleadoPorCodigoServicio(codigoOrNombreEmpleado);
            return Ok(data);
        }
    }
}

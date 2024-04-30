using Domain.Dtos;
using Domain.Entidades.Empleados;
using Domain.Servicios.EmpleadosServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class EmpleadoServicioAplicacion : IServiceEmpleadoAplicacion
    {
        private IEmpleadoServicioDomain _servicioDomain;

        public EmpleadoServicioAplicacion(IEmpleadoServicioDomain servicioDomain)
        {
           _servicioDomain = servicioDomain;   
        }

        public async Task<ResponseDomainDTO> AgregaEmpleadoServicio(EmpleadoDTO empleado)
        {
           var res = await _servicioDomain.AgregarEmpleado(empleado);
            return res;
        }

        public Task<ResponseDomainDTO> EditarEmpleadoServicio(EmpleadoDTO empleado)
        {
           var res = _servicioDomain.EditarEmpleado(empleado);
            return res;
        }

        public async Task<ResponseDomainDTO> EliminarEmpleadServicio(int codigoEmpleado, int lockerAsignado)
        {
          var res =  await _servicioDomain.EliminarEmpleado(codigoEmpleado,lockerAsignado);
            return res;
        }

        public async Task<Empleado> EmpleadoPorCodigoServicio(int codigoEmpleado)
        {
          var res = await _servicioDomain.EmpleadoPorCodigo(codigoEmpleado);
          return res;
        }

        public async Task<IEnumerable<Empleado>> GetEmpleadosServicios()
        {
           var empleados = await _servicioDomain.GetEmpleados();
            return empleados;
        }
    }
}

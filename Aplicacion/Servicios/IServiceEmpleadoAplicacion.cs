using Domain.Dtos;
using Domain.Entidades.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public interface IServiceEmpleadoAplicacion
    {
        Task<IEnumerable<Empleado>> GetEmpleadosServicios();
        Task<ResponseDomainDTO> AgregaEmpleadoServicio(EmpleadoDTO empleado);
        Task<ResponseDomainDTO> EditarEmpleadoServicio(EmpleadoDTO empleado);
        Task<ResponseDomainDTO> EliminarEmpleadServicio(int codigoEmpleado,int lockerAsignado);
        Task<Empleado> EmpleadoPorCodigoServicio(int codigoEmpleado);
    }
}

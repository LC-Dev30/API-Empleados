using Domain.Dtos;
using Domain.Entidades.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicios.EmpleadosServicios
{
    public interface IEmpleadoServicioDomain
    {
        Task<IEnumerable<Empleado>> GetEmpleados();
        Task<ResponseDomainDTO> AgregarEmpleado(EmpleadoDTO empleado);
        Task<ResponseDomainDTO> EditarEmpleado(EmpleadoDTO empleado);
        Task<ResponseDomainDTO> EliminarEmpleado(int codigoEmpleado,int lockerAsignado);
        Task<Empleado> EmpleadoPorCodigo(string codigoOrNombreEmpleado);
    }
}

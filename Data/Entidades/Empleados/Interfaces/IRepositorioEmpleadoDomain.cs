

using Domain.Dtos;

namespace Domain.Entidades.Empleados.Interfaces
{
    public interface IRepositorioEmpleadoDomain
    {
        Task<IEnumerable<Empleado>> ListaEmpleados();
        Task<ResponseDomainDTO> AgregarEmpleado(Empleado empleado);
        Task<ResponseDomainDTO> EditarEmpleado(EmpleadoDTO empleado);
        Task<ResponseDomainDTO> EliminarEmpleado(int CodigoEmpleado);
        Task<Empleado> EmpleadoPorCodigo(int CodigoEmpleado);
    }
}

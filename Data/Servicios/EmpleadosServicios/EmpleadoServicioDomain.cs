using Domain.Common;
using Domain.Dtos;
using Domain.Entidades.Empleados;
using Domain.Entidades.Empleados.Interfaces;
using Domain.Servicios.LockersServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicios.EmpleadosServicios
{
    public class EmpleadoServicioDomain : IEmpleadoServicioDomain
    {
        private readonly IRepositorioEmpleadoDomain _rep;
        private readonly IServicioLockerDomain _Serviciolocker;

        public EmpleadoServicioDomain(IRepositorioEmpleadoDomain rep, IServicioLockerDomain Serviciolocker)
        {
            _rep = rep;
            _Serviciolocker = Serviciolocker;
        }

        public async Task<ResponseDomainDTO> AgregarEmpleado(EmpleadoDTO empleado)
        {
            var emp = new Empleado()
            {
                FechaCreacion = DateTime.Now,
                IdAdmin = 1,
                Nombre = empleado.Nombre,
                LockerAsignado = empleado.LockerAsignado,
                CodigoEmpleado = CommonDomain.CodigoEmpleado()
            };

            var res = await _rep.AgregarEmpleado(emp);
            await _Serviciolocker.AsignarLockerDomain(empleado.LockerAsignado);
            return res;
        }

        public async Task<ResponseDomainDTO> EditarEmpleado(EmpleadoDTO empleado)
        {
            if(empleado.NuevoLocker != 0)
            {
              await _Serviciolocker.LiberarLockerDomain(empleado.LockerAsignado);
              await _Serviciolocker.AsignarLockerDomain(empleado.NuevoLocker);
              empleado.LockerAsignado = empleado.NuevoLocker;
            }

            var res = await _rep.EditarEmpleado(empleado);
            return res;
        }

        public async Task<ResponseDomainDTO> EliminarEmpleado(int codigoEmpleado, int lockerAsignado)
        {
           var res = await _rep.EliminarEmpleado(codigoEmpleado);
           await _Serviciolocker.LiberarLockerDomain(lockerAsignado);
            return res;
        }

        public async Task<Empleado> EmpleadoPorCodigo(string codigoOrNombreEmpleado)
        {
            var res = await _rep.EmpleadoPorCodigo(codigoOrNombreEmpleado);
            return res;
        }

        public async Task<IEnumerable<Empleado>> GetEmpleados()
        {
            var data = await _rep.ListaEmpleados();
            return data;
        }
    }
}

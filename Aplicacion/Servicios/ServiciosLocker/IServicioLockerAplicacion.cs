using Domain.Dtos;
using Domain.Entidades.Lockers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.ServiciosLocker
{
    public interface IServicioLockerAplicacion
    {
        Task<IEnumerable<Lockers>> ListaLockersServicio();
        Task<ResponseDomainDTO> AsignarLockerServicio(int numLocker);
        Task<ResponseDomainDTO> AgregarLockerServicio(int numLockers);
    }
}

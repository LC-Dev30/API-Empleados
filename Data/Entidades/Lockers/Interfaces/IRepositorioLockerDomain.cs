using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades.Lockers.Interfaces
{
    public interface IRepositorioLockerDomain
    {
        Task<IEnumerable<Lockers>> ListadoLockers();
        Task<ResponseDomainDTO> AsignarLocker(int numLocker);
        Task<ResponseDomainDTO> AgregarLockers(Lockers locker);
        Task<ResponseDomainDTO> LiberarLocker(int numLocker);
        Task<IEnumerable<Lockers>> ListadoLockersSinFiltro();
    }
}

using Domain.Dtos;
using Domain.Entidades.Lockers;


namespace Domain.Servicios.LockersServicios
{
    public interface IServicioLockerDomain
    {
       Task<IEnumerable<Lockers>> ListaLockersDomain();
        Task<ResponseDomainDTO> AsignarLockerDomain(int numLocker);
        Task<ResponseDomainDTO> AgregarLockerDomain(int numLocker);
        Task<ResponseDomainDTO> LiberarLockerDomain(int numLocker);
        Task<IEnumerable<Lockers>> ListadoLockersSinFiltroDomain();
    }
}

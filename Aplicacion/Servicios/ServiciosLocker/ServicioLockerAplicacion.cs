using Domain.Dtos;
using Domain.Entidades.Lockers;
using Domain.Servicios.LockersServicios;

namespace Aplicacion.Servicios.ServiciosLocker
{
    public class ServicioLockerAplicacion : IServicioLockerAplicacion
    {
        private readonly IServicioLockerDomain _servicioLockerDomain;

        public ServicioLockerAplicacion(IServicioLockerDomain servicioLockerDomain)
        {
            _servicioLockerDomain = servicioLockerDomain;    
        }

        public Task<ResponseDomainDTO> AgregarLockerServicio(int numLockers)
        {
           var res = _servicioLockerDomain.AgregarLockerDomain(numLockers);
           return res;
        }

        public async Task<ResponseDomainDTO> AsignarLockerServicio(int numLocker)
        {
            var asigLocker = await _servicioLockerDomain.AsignarLockerDomain(numLocker);
            return asigLocker;  
        }

        public async Task<IEnumerable<Lockers>> ListaLockersServicio()
        {
           var lstLockers = await _servicioLockerDomain.ListaLockersDomain();
            return lstLockers;  
        }
    }
}

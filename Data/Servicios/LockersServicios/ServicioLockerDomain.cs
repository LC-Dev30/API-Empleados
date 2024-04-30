using Domain.Common.Enums;
using Domain.Dtos;
using Domain.Entidades.Lockers;
using Domain.Entidades.Lockers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicios.LockersServicios
{
    public class ServicioLockerDomain : IServicioLockerDomain
    {
        private IRepositorioLockerDomain _repositorioLocker;

        public ServicioLockerDomain(IRepositorioLockerDomain repositorioLocker)
        {
           _repositorioLocker = repositorioLocker;
        }

        public async Task<ResponseDomainDTO> AgregarLockerDomain(int numLocker)
        {
            var lstLockers = await _repositorioLocker.ListadoLockersSinFiltro();
            var ultimoNumeroLocker = lstLockers.Any() ? lstLockers.Max(l => l.NumeroLocker) + 1 : 1;

            for (int i = ultimoNumeroLocker; i < ultimoNumeroLocker + numLocker; i++)
            {
                var locker = new Lockers() { NumeroLocker = i,BandejaPocision = i,CalzadoPocision = i, Estado = EnumIsEmpty.Vacio };
                await _repositorioLocker.AgregarLockers(locker);
            }

            throw new Exception("");
        }



        public Task<ResponseDomainDTO> AsignarLockerDomain(int numLocker)
        {
            var asigLoc = _repositorioLocker.AsignarLocker(numLocker);
            return asigLoc;
        }

        public async Task<ResponseDomainDTO> LiberarLockerDomain(int numLocker)
        {
          var res = await _repositorioLocker.LiberarLocker(numLocker);
            return res;
        }

        public async Task<IEnumerable<Lockers>> ListadoLockersSinFiltroDomain()
        {
           var lstLockerSinFiltro = await _repositorioLocker.ListadoLockersSinFiltro();
            return lstLockerSinFiltro;
        }

        public async Task<IEnumerable<Lockers>> ListaLockersDomain()
        {
          var lstLockers = await _repositorioLocker.ListadoLockers();
          return lstLockers;
        }
    }
}

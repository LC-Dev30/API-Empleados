using Domain.Dtos;
using Domain.Entidades.Admin;

namespace Infraestructura.Servicios.Login
{
    public interface ILoginServicio
    {
        Task<ResponseDomainDTO> LoginService(Admin admin);
    }
}

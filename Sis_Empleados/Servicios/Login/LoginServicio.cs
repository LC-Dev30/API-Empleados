using Domain.Dtos;
using Domain.Entidades.Admin;
using System.Data.SqlClient;

namespace Infraestructura.Servicios.Login
{
    public class LoginServicio : ILoginServicio
    {
        IConfiguration _configuration;
        SqlConnection _connection;

        public LoginServicio(IConfiguration configuration)
        {
           _configuration = configuration;
           _connection = new SqlConnection(_configuration.GetConnectionString("Conexion"));
        }

        public async Task<ResponseDomainDTO> LoginService(Admin admin)
        {
            var IsValid = ValidarAdmin(admin);

            if (!IsValid)
                return new ResponseDomainDTO() { StatusCode = 400, Message = "Usuario o Contraseña Invalidas" };

            using (_connection)
            {
               await _connection.OpenAsync();
                var cmd = _connection.CreateCommand();
                cmd.CommandText = "SELECT TOP 1 Id,Usuario FROM Admins WHERE Usuario = @Usuario AND Password = @Password";

                cmd.Parameters.AddWithValue("@Usuario", admin.Nombre);
                cmd.Parameters.AddWithValue("@Password", admin.Contraseña);

                var dataReader = await cmd.ExecuteReaderAsync();

                var adminModel = new Admin();

                if(await dataReader.ReadAsync())
                {             
                    adminModel.Id = dataReader.GetInt32(0);
                    adminModel.Nombre = dataReader.GetString(1);         
                    return new ResponseDomainDTO { Data = adminModel,StatusCode = 200 };
                }
                else
                {
                  return new ResponseDomainDTO { Data = null, StatusCode = 404, Message = "Usuario no encontrado" };
                }
            }
        }

        private bool ValidarAdmin(Admin admin)
        {
            if (string.IsNullOrEmpty(admin.Nombre) || admin.Nombre.Length > 50
                || string.IsNullOrEmpty(admin.Contraseña) || admin.Contraseña.Length < 8)
            {
                return false;
            }
                        
            return true;
        }
    }
}

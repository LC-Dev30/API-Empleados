using Domain.Dtos;
using Domain.Entidades.Lockers;
using Domain.Entidades.Lockers.Interfaces;
using System.Data.SqlClient;

namespace Infraestructura.Persistencia.Repositorios
{
    public class LockerRepositorio : IRepositorioLockerDomain
    {

        private readonly SqlConnection _connection;
        private readonly IConfiguration _config;

        public LockerRepositorio(IConfiguration config)
        {
            _config = config;
            string connectionString = _config.GetConnectionString("Conexion");
            _connection = new SqlConnection();
            _connection.ConnectionString = connectionString;
        }

        public async Task<ResponseDomainDTO> AgregarLockers(Lockers locker)
        {
           using(_connection)
           {
                _connection.ConnectionString = _config.GetConnectionString("Conexion");
                await _connection.OpenAsync();
                var cmd = _connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Locker VALUES(@NumeroLocker,@Estado,@BandejaPocision,@CalzadoPocision)";

                cmd.Parameters.AddWithValue("@NumeroLocker", locker.NumeroLocker);
                cmd.Parameters.AddWithValue("@Estado", locker.Estado);
                cmd.Parameters.AddWithValue("BandejaPocision", locker.BandejaPocision);
                cmd.Parameters.AddWithValue("@CalzadoPocision", locker.CalzadoPocision);

                var IsAgregado = await cmd.ExecuteNonQueryAsync();
                await _connection.CloseAsync();
                if (IsAgregado == 0)
                    return new ResponseDomainDTO { StatusCode = 500, Message = $"No se pudo agregar el Locker {locker.NumeroLocker}" };

                return new ResponseDomainDTO { StatusCode = 200, Message = "Locker agregado correctamente!" };
            }
        }

        public async Task<ResponseDomainDTO> AsignarLocker(int numLocker)
        {
            try
            {
                using (_connection)
                {
                    _connection.ConnectionString = _config.GetConnectionString("Conexion");
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();
                    cmd.CommandText = "UPDATE Locker SET Estado = 1 WHERE NumeroLocker = @numLocker";

                    cmd.Parameters.AddWithValue("@numLocker", numLocker);
                    var IsAgregado = await cmd.ExecuteNonQueryAsync();

                    await _connection.CloseAsync();
                    if (IsAgregado == 0)
                        return new ResponseDomainDTO { StatusCode = 500, Message = "No se pudo asinar el Locker" };

                    return new ResponseDomainDTO { StatusCode = 200, Message = "Locker Asignado correctamente!" };
                }
            }
            catch (Exception Err)
            {
               return new ResponseDomainDTO { StatusCode = 500, Message = $"Error: {Err.Message}" };
            }
        }

        public async Task<IEnumerable<Lockers>> ListadoLockers()
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();
                    cmd.CommandText = "SELECT NumeroLocker FROM Locker WHERE Estado = 0";

                    var reader = await cmd.ExecuteReaderAsync();
                    var lstLocker = new List<Lockers>();

                    while (await reader.ReadAsync())
                    {
                        var locker = new Lockers() { NumeroLocker = reader.GetInt32(0) };
                        lstLocker.Add(locker);
                    }

                    await _connection.CloseAsync();
                    return lstLocker;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDomainDTO> LiberarLocker(int numLocker)
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();
                    cmd.CommandText = "UPDATE Locker SET Estado = 0 WHERE NumeroLocker = @numeroLocker";

                    cmd.Parameters.AddWithValue("@numeroLocker", numLocker);

                    var IsAgregado = await cmd.ExecuteNonQueryAsync();

                    await _connection.CloseAsync();
                    if (IsAgregado == 0)
                        return new ResponseDomainDTO { StatusCode = 500, Message = "No se pudo liberar el Locker" };

                    return new ResponseDomainDTO { StatusCode = 200, Message = "Locker liberado correctamente!" };
                }
            }
            catch (Exception Err)
            {
                return new ResponseDomainDTO { StatusCode = 500, Message = $"Error: {Err.Message}" };
            }
        }

        public async Task<IEnumerable<Lockers>> ListadoLockersSinFiltro()
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();
                    cmd.CommandText = "SELECT NumeroLocker FROM Locker";

                    var reader = await cmd.ExecuteReaderAsync();
                    var lstLocker = new List<Lockers>();

                    while (await reader.ReadAsync())
                    {
                        var locker = new Lockers() { NumeroLocker = reader.GetInt32(0) };
                        lstLocker.Add(locker);
                    }

                    await _connection.CloseAsync();
                    return lstLocker;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

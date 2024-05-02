using Domain.Dtos;
using Domain.Entidades.Empleados;
using Domain.Entidades.Empleados.Interfaces;
using System.Data.SqlClient;

namespace Arquitectura.Persistencia.Repositorios
{
    public class EmpleadoRepositorio : IRepositorioEmpleadoDomain
    {
        private readonly SqlConnection _connection;
        private readonly IConfiguration _config;
        public EmpleadoRepositorio(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config.GetConnectionString("Conexion"));
        }

        public async Task<ResponseDomainDTO> AgregarEmpleado(Empleado empleado)
        {
            try
            {
                using (_connection)
                {
                    var cmd = _connection.CreateCommand();
                    
                    await _connection.OpenAsync();
                    cmd.CommandText = "INSERT INTO Empleado VALUES(@CodigoEmpleado,@Nombre,@LockerAsignado,@IdAdmin,@FechaCreacion)";

                    cmd.Parameters.AddWithValue("@CodigoEmpleado", empleado.CodigoEmpleado);
                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre.ToUpper());
                    cmd.Parameters.AddWithValue("@LockerAsignado", empleado.LockerAsignado);
                    cmd.Parameters.AddWithValue("@IdAdmin", empleado.IdAdmin);
                    cmd.Parameters.AddWithValue("@FechaCreacion", empleado.FechaCreacion);

                    var IsAgregado = await cmd.ExecuteNonQueryAsync();

                    await _connection.CloseAsync();
                    if (IsAgregado == 0)
                        return new ResponseDomainDTO { StatusCode = 500, Message = "El empleado no pudo ser registrado correctamente, vuelva a intertarlo." };

                    return new ResponseDomainDTO { StatusCode = 201, Message = "Empleado Agregado!" };  
                }
            }
            catch (Exception Err)
            {
                await _connection.CloseAsync();
                return new ResponseDomainDTO { StatusCode = 500, Message = $"Err: {Err.Message}" };
            }
        }

        public async Task<ResponseDomainDTO> EditarEmpleado(EmpleadoDTO empleado)
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();

                    cmd.CommandText = "UPDATE Empleado SET Nombre = @Nombre, LockerAsignado = @LockerAsignado WHERE CodigoEmpleado = @CodigoEmpleado";

                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@LockerAsignado", empleado.LockerAsignado);
                    cmd.Parameters.AddWithValue("@CodigoEmpleado", empleado.CodigoEmpleado);

                    var IsAgregado = await cmd.ExecuteNonQueryAsync();

                    await _connection.CloseAsync();
                    if (IsAgregado == 0)
                        return new ResponseDomainDTO { StatusCode = 500, Message = "El empleado no pudo ser editado correctamente, vuelva a intertarlo." };

                    return new ResponseDomainDTO { StatusCode = 200, Message = "Empleado Editado!" };
                }
            }
            catch (Exception Err)
            {
                await _connection.CloseAsync();
                return new ResponseDomainDTO { StatusCode = 500, Message = $"Err: {Err.Message}" };
            }
        }

        public async Task<ResponseDomainDTO> EliminarEmpleado(int CodigoEmpleado)
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();
                    cmd.CommandText = "DELETE FROM Empleado WHERE CodigoEmpleado = @codigoEmpleado";

                    cmd.Parameters.AddWithValue("@codigoEmpleado", CodigoEmpleado);

                    var IsAgregado = await cmd.ExecuteNonQueryAsync();

                    await _connection.CloseAsync();
                    if (IsAgregado == 0)
                        return new ResponseDomainDTO { StatusCode = 500, Message = "El empleado no pudo ser eliminado correctamente, vuelva a intertarlo." };

                    return new ResponseDomainDTO { StatusCode = 200, Message = "Empleado Eliminado!" };
                }
            }
            catch (Exception Err)
            {
                await _connection.CloseAsync();
                return new ResponseDomainDTO { StatusCode = 500, Message = $"Err {Err.Message}" };
            }
        }

        public async Task<Empleado> EmpleadoPorCodigo(string codigoOrNombreEmpleado)
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();

                    var query = "SELECT TOP 1 Nombre,FechaCreacion,LockerAsignado,CodigoEmpleado FROM Empleado WHERE CodigoEmpleado = @codigoEmpleado";

                    if(codigoOrNombreEmpleado.Length > 4)
                    {
                        query = "SELECT TOP 1 Nombre,FechaCreacion,LockerAsignado,CodigoEmpleado FROM Empleado WHERE Nombre = @Nombre";
                        cmd.Parameters.AddWithValue("@Nombre", codigoOrNombreEmpleado);
                    }

                    if(codigoOrNombreEmpleado.Length == 4)
                      cmd.Parameters.AddWithValue("@codigoEmpleado", codigoOrNombreEmpleado);
         
                    cmd.CommandText = query;

                    var reader = await cmd.ExecuteReaderAsync();

                    Empleado empleado = new Empleado();

                    if (await reader.ReadAsync())
                    {
                        empleado.Nombre = reader.GetString(0);
                        empleado.FechaCreacion = reader.GetDateTime(1);
                        empleado.LockerAsignado = reader.GetInt32(2);
                        empleado.CodigoEmpleado = reader.GetString(3);
                        return empleado;
                    }

                    await _connection.CloseAsync();
                    return null;
                }
            }
            catch (Exception)
            {
                await _connection.CloseAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Empleado>> ListaEmpleados()
        {
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    var cmd = _connection.CreateCommand();
                    cmd.CommandText = "SELECT Nombre,FechaCreacion,LockerAsignado,CodigoEmpleado FROM Empleado";

                    var reader = await cmd.ExecuteReaderAsync();
                    var lstEmpleados = new List<Empleado>();

                    while (await reader.ReadAsync())
                    {
                        var empleado = new Empleado() {
                            Nombre = reader.GetString(0),
                            FechaCreacion = reader.GetDateTime(1),
                            LockerAsignado = reader.GetInt32(2),
                            CodigoEmpleado = reader.GetString(3),
                        };
                        lstEmpleados.Add(empleado);
                    }
                    await _connection.CloseAsync();
                    return lstEmpleados;
                }
            }
            catch (Exception)
            {
                await _connection.CloseAsync();
                throw;
            }
        }
    }
}

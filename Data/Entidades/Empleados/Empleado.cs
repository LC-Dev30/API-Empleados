

namespace Domain.Entidades.Empleados
{
    public class Empleado
    {
        public int Id { get; set; }
        public string CodigoEmpleado { get; set; }
        public string Nombre { get; set; }
        public int LockerAsignado { get; set; }
        public int IdAdmin { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

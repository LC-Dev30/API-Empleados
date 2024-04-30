using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class EmpleadoDTO
    {
        public string Nombre { get; set; }
        public int LockerAsignado { get; set; }
        public string CodigoEmpleado { get; set; }
        public int NuevoLocker { get; set; }
    }
}

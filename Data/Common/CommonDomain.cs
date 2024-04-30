using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public static class CommonDomain
    {
        public static string CodigoEmpleado()
        {
           return new Random().Next(1000,10000).ToString();
        }
    }
}

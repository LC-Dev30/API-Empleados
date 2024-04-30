
using Domain.Common.Enums;

namespace Domain.Entidades.Lockers
{
    public class Lockers
    {
        public int Id { get; set; }
        public int NumeroLocker { get; set; }
        public EnumIsEmpty Estado { get; set; }
        public int BandejaPocision { get; set; }
        public int CalzadoPocision { get; set; }

    }
}

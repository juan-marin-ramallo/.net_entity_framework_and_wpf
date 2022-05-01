using System;
using System.Collections.Generic;

namespace EntidadEntityFramework
{
    public partial class Coche
    {
        public Coche()
        {
            ReservaCoches = new HashSet<ReservaCoche>();
        }

        public string Placa { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Color { get; set; } = null!;
        public decimal PrecioHoraAlquiler { get; set; }

        public virtual ICollection<ReservaCoche> ReservaCoches { get; set; }
    }
}

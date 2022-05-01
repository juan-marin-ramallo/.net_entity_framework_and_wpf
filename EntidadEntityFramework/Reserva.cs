using System;
using System.Collections.Generic;

namespace EntidadEntityFramework
{
    public partial class Reserva
    {
        public Reserva()
        {
            ReservaCoches = new HashSet<ReservaCoche>();
        }

        public int NumeroReserva { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioTotalReserva { get; set; }
        public int CodCliente { get; set; }

        public virtual Cliente CodClienteNavigation { get; set; } = null!;
        public virtual ICollection<ReservaCoche> ReservaCoches { get; set; }
    }
}

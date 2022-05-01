using System;
using System.Collections.Generic;

namespace EntidadEntityFramework
{
    public partial class ReservaCoche
    {
        public int NumeroReserva { get; set; }
        public string Placa { get; set; } = null!;
        public byte GalonesGasolina { get; set; }

        public virtual Reserva NumeroReservaNavigation { get; set; } = null!;
        public virtual Coche PlacaNavigation { get; set; } = null!;
    }
}

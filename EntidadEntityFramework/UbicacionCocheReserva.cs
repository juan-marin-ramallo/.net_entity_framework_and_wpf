using System;
using System.Collections.Generic;

namespace EntidadEntityFramework
{
    public partial class UbicacionCocheReserva
    {
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public DateTime FechaHora { get; set; }
        public int NumeroReserva { get; set; }
        public string Placa { get; set; } = null!;

        public virtual ReservaCoche ReservaCoche { get; set; } = null!;
    }
}

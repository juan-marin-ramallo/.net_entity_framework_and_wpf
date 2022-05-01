using System;
using System.Collections.Generic;

namespace DatosEntityFramework
{
    public partial class VwClienteReservaCoche
    {
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int NumeroReserva { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioTotalReserva { get; set; }
        public string Placa { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public decimal PrecioHoraAlquiler { get; set; }
        public byte GalonesGasolina { get; set; }
    }
}

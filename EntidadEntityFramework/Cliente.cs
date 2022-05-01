using System;
using System.Collections.Generic;

namespace EntidadEntityFramework
{
    public partial class Cliente
    {
        public Cliente()
        {
            InverseCodGaranteNavigation = new HashSet<Cliente>();
            Reservas = new HashSet<Reserva>();
        }

        public int CodCliente { get; set; }
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public byte Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public int? CodGarante { get; set; }

        public virtual Cliente? CodGaranteNavigation { get; set; }
        public virtual ICollection<Cliente> InverseCodGaranteNavigation { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}

using System.Linq;
using EntidadEntityFramework;
using DatosEntityFramework;

namespace Controlador
{
    public class ClienteControlador
    {
        public bool insertarCliente(Cliente cliente) { 
            bool resultado = false;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext()) {
                    contextDB.Clientes.Add(cliente);
                    contextDB.SaveChanges(); //parecido al Commit
                    resultado = true;                    
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return resultado;
        }

        public bool actualizarCliente(Cliente cliente) {
            bool resultado = false;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext())
                {
                    contextDB.Clientes.Update(cliente);
                    contextDB.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return resultado;
        }

        public bool eliminarCliente(Cliente cliente) {
            bool resultado = false;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext())
                {
                    contextDB.Clientes.Remove(cliente);
                    contextDB.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return resultado;
        }

        public Cliente? consultarClientePorCedula(String cedula) {
            Cliente? cliente = null;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext())
                {
                    cliente = contextDB.Clientes.Where(c => c.Cedula == cedula).First();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return cliente;
        }

        public Cliente? consultarClientePorCodigo(Int32 codCliente)
        {
            Cliente? cliente = null;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext())
                {
                    cliente = contextDB.Clientes.Find(codCliente);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return cliente;
        }

        public List<Cliente>? consultarTodosLosClientes() {
            List<Cliente>? listaCLientes = null;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext())
                {
                    var query = from c in contextDB.Clientes
                                select c;

                    listaCLientes = query.ToList();

                    //listaCLientes = contextDB.Clientes.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return listaCLientes;
        }

        public List<Cliente>? consultarClientesPorGarante(Int32 codGarante) {
            List<Cliente>? listaClientesPorGarante = null;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext()) { 
                    listaClientesPorGarante = contextDB.Clientes.Where(c => c.CodGarante == codGarante).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return listaClientesPorGarante;
        }

        public List<Cliente>? consultarGarantesDisponibles(Int32 codCliente)
        {
            List<Cliente>? listaGarantesDisponibles = null;

            try
            {
                using (ReservaCochesContext contextDB = new ReservaCochesContext())
                {
                    List<Cliente> listaClientesGarantizados = contextDB.Clientes.Where(c => c.CodGarante == codCliente).ToList();

                    listaGarantesDisponibles = contextDB.Clientes.Where(c => c.CodCliente != codCliente && !listaClientesGarantizados.Contains(c)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }

            return listaGarantesDisponibles;
        }
    }
}
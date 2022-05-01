using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EntidadEntityFramework;
using Controlador;

namespace VistaWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cliente? cliente, garante;
        List<Cliente>? garantesDisponibles;
        ClienteControlador clienteControlador;

        public MainWindow()
        {
            InitializeComponent();
            clienteControlador = new ClienteControlador();
            inicializarFomrulario();
        }

        private void inicializarFomrulario() { 
            dgCliente.ItemsSource = clienteControlador.consultarTodosLosClientes();

            cmbGarante.ItemsSource = clienteControlador.consultarTodosLosClientes();
            cmbGarante.DisplayMemberPath = "Nombre";
            cmbGarante.SelectedValuePath = "CodCliente";
            cmbGarante.SelectedIndex = 0;
            cmbGarante.IsEnabled = true;

            chbTieneGarante.IsChecked = true;   

            txtCedula.Text = String.Empty;
            txtNombre.Text = String.Empty;  
            txtEdad.Text = String.Empty;
            txtDireccion.Text = String.Empty;   
            txtTelefono.Text = String.Empty;   
            
            cliente = new Cliente();
            gridCliente.DataContext = cliente;
            txtCedula.Focus();
        }

        private void cargarComboBoxConGarantesDisponibles(Int32 codCliente) {
            try
            {
                garantesDisponibles = clienteControlador.consultarGarantesDisponibles(codCliente);
                cmbGarante.ItemsSource = garantesDisponibles;
                cmbGarante.DisplayMemberPath = "Nombre";
                cmbGarante.SelectedValuePath = "CodCliente";
                cmbGarante.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema al consultar garantes disponibles: " + ex.Message, "Mensaje de Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void miNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            inicializarFomrulario();
        }

        private void miCrearCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCedula.Equals(""))
                    MessageBox.Show("Por favor ingrese la cedula", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (txtNombre.Equals(""))
                    MessageBox.Show("Por favor ingrese el nombre", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (txtEdad.Equals(""))
                    MessageBox.Show("Por favor ingrese la edad", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                else {
                    if (chbTieneGarante.IsChecked == true) {
                        garante = (Cliente) cmbGarante.SelectedItem;
                        cliente!.CodGarante = garante.CodCliente;
                    }

                    if (clienteControlador.insertarCliente(cliente!))
                        MessageBox.Show("Cliente ingresado con exito", "Mensaje de Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Cliente no pudo ser ingresado", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

                    inicializarFomrulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema al ingresar cliente: " + ex.Message,"Mensaje de Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void miModificarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCedula.Equals(""))
                    MessageBox.Show("Por favor ingrese la cedula", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (txtNombre.Equals(""))
                    MessageBox.Show("Por favor ingrese el nombre", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                else if (txtEdad.Equals(""))
                    MessageBox.Show("Por favor ingrese la edad", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (chbTieneGarante.IsChecked == true)
                    {
                        garante = (Cliente)cmbGarante.SelectedItem;
                        cliente!.CodGarante = garante.CodCliente;
                    }
                    else {
                        cliente!.CodGarante = null;
                        cliente!.CodGaranteNavigation = null;
                    }

                    if (clienteControlador.actualizarCliente(cliente!))
                        MessageBox.Show("Cliente actualizado con exito", "Mensaje de Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Cliente no pudo ser actualizado", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);

                    inicializarFomrulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema al actualizar cliente: " + ex.Message, "Mensaje de Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void miSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEditarCliente_Click(object sender, RoutedEventArgs e)
        {
            cliente = (sender as FrameworkElement)!.DataContext as Cliente;

            //cliente = (Cliente) ((FrameworkElement)sender)!.DataContext;

            gridCliente.DataContext = cliente;

            cargarComboBoxConGarantesDisponibles(cliente!.CodCliente);

            if (cliente.CodGarante != null)
            {
                garante = cliente.CodGaranteNavigation;

                chbTieneGarante.IsChecked = true;

                int posicionGaranteEnElCombo = garantesDisponibles!.FindIndex(c => c.Nombre.Equals(garante!.Nombre));
                cmbGarante.SelectedIndex = posicionGaranteEnElCombo;
            }
            else
                chbTieneGarante.IsChecked = false;
        }

        private void btnEliminaCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult confirmacionEliminar = 
                    MessageBox.Show("Esta seguro que desea eliminar al cliente?", "Eliminacion de Clientes", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmacionEliminar == MessageBoxResult.Yes) {
                    cliente = (sender as FrameworkElement)!.DataContext as Cliente;

                    List<Cliente>? clientesGarantizados = clienteControlador.consultarClientesPorGarante(cliente!.CodCliente);

                    if (clientesGarantizados!.Count > 0)
                        MessageBox.Show("Cliente no puede ser eliminado por ser Garante", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    else {
                        if (clienteControlador.eliminarCliente(cliente))
                        {
                            MessageBox.Show("Cliente eliminado con exito", "Mensaje de Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                            dgCliente.ItemsSource = clienteControlador.consultarTodosLosClientes();
                        }
                        else
                            MessageBox.Show("Cliente no puede ser eliminado", "Mensaje de Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problema al eliminar cliente: " + ex.Message, "Mensaje de Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void chbTieneGarante_Checked(object sender, RoutedEventArgs e)
        {
            if (chbTieneGarante.IsChecked == true)
                cmbGarante.IsEnabled = true;
            else
                cmbGarante.IsEnabled = false;
        }
    }
}

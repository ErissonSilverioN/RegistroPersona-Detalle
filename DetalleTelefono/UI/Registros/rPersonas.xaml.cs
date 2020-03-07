using DetalleTelefono.BLL;
using DetalleTelefono.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DetalleTelefono.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPersonas.xaml
    /// </summary>
    public partial class rPersonas : Window
    {
        public List<TelefonosDetalle> Detalles { get; set; }
        public rPersonas()
        {
            InitializeComponent();
            this.Detalles = new List<TelefonosDetalle>();
            IdTextBox.Text = "0";
        }

        private void Limpiar()
        {
            
            nombreTextBox.Text = string.Empty;
            direccionTextBox.Text = string.Empty;
            cedulaTextBox.Text = string.Empty;
            fechanacDatePicker.SelectedDate = DateTime.Now;
            telefonoTextBox.Text = string.Empty;
            tipotelefonoTextBox.Text = string.Empty;
            IdTextBox.Text = "0";

            this.Detalles = new List<TelefonosDetalle>();
            CargarDataGrid();
        }

        private Personas LlenaClase()
        {
            Personas personas = new Personas();
            personas.PersonaId = Convert.ToInt32(IdTextBox.Text);
            personas.Nombre = nombreTextBox.Text;
            personas.Cedula = cedulaTextBox.Text;
            personas.Direccion = direccionTextBox.Text;
            personas.FechaNacimiento = fechanacDatePicker.SelectedDate.Value;

            personas.Detalles = this.Detalles;

            return personas;
        }

        private void LlenaCampos(Personas personas)
        {
            IdTextBox.Text = Convert.ToString(personas.PersonaId);
            nombreTextBox.Text = personas.Nombre;
            direccionTextBox.Text = personas.Direccion;
            cedulaTextBox.Text = Convert.ToString(personas.Cedula);
            fechanacDatePicker.SelectedDate = personas.FechaNacimiento;

            this.Detalles = personas.Detalles;
            CargarDataGrid();

        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Personas persona = PersonaBLL.Buscar(Convert.ToInt32(IdTextBox.Text));
            return (persona != null);
        }

        private bool Validar()
        {
            bool paso = true;

            if (nombreTextBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo Nombre Es Obligatorio");
                nombreTextBox.Focus();
                paso = false;

            }
            

            if (string.IsNullOrWhiteSpace(direccionTextBox.Text))
            {
                MessageBox.Show("El Campo Direccion Es Obligatorio");
                direccionTextBox.Focus();
                paso = false;
            }
            if (fechanacDatePicker.Text == string.Empty)
            {
                MessageBox.Show("Debe Seleccionar una Fecha");
                fechanacDatePicker.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(cedulaTextBox.Text))
            {
                MessageBox.Show("El Campo Cadula Es Obligatorio");
                cedulaTextBox.Focus();
                paso = false;
            }
            if (telefonodetalleDataGrid.Columns.Count == 0)
            {
                MessageBox.Show("Debe Agregar Datos al DataGrid!!!!");
                telefonodetalleDataGrid.Focus();
                paso = false;
            }
            return paso;
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas personas;
            bool paso = false;
            if (!Validar())
                return;

            personas = LlenaClase();

            if (Convert.ToInt32(IdTextBox.Text) == 0)
                paso = PersonaBLL.Guardar(personas);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede Modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonaBLL.Modificar(personas);
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo");
        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas personas = new Personas();
            int.TryParse(IdTextBox.Text, out id);
            personas = PersonaBLL.Buscar(id);

            if (personas != null)
            {
                MessageBox.Show("Persona Encontrada", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                LlenaCampos(personas);

            }
            else
            {
                MessageBox.Show("Persona no econtrada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
            }
        }

        private void eliminarButton__Click(object sender, RoutedEventArgs e)
        {

            int id;
            int.TryParse(IdTextBox.Text, out id);

            Limpiar();
            if (PersonaBLL.Eliminar(id))

                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Erro Al eliminar una persona");
        }

        private void CargarDataGrid()
        {
            telefonodetalleDataGrid.ItemsSource = null;
            telefonodetalleDataGrid.ItemsSource = this.Detalles;
        }

        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (telefonodetalleDataGrid.SelectedItem != null)
                this.Detalles = (List<TelefonosDetalle>)telefonodetalleDataGrid.ItemsSource;


            this.Detalles.Add(
                new TelefonosDetalle(
                    id: 0,
                    personaId: Convert.ToInt32(IdTextBox.Text),
                    tipoTelefono: tipotelefonoTextBox.Text,
                    telefono: telefonoTextBox.Text

                    ));

            CargarDataGrid();
            telefonoTextBox.Clear();
            tipotelefonoTextBox.Clear();
        }

        private void removerButton_Click(object sender, RoutedEventArgs e)
        {
            if (telefonodetalleDataGrid.Columns.Count > 0 && telefonodetalleDataGrid.SelectedCells != null)
            {
                Detalles.RemoveAt(telefonodetalleDataGrid.SelectedIndex);
                CargarDataGrid();
            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void nombreTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }

        }

        private void direccionTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }

        }

        private void cedulaTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void telefonoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void tipotelefonoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }

        }

        private void IdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}

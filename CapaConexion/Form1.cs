using DatosLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaConexion
{
    public partial class Form1 : Form
    {
        CustomerRepository customerRepository = new CustomerRepository();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            var Customers = customerRepository.ObtenerTodos();
            dataGrid.DataSource = Customers;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //var filtro = Customers.FindAll(X => X.CompanyName.StartsWith(textBox1.Text));
            //dataGrid.DataSource = filtro;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*DatosLayer.DataBase.ApplicationName = "Programacion 2 ejemplo";
            DatosLayer.DataBase.ConnetionTimeout = 30;

            string cadenaConexion = DatosLayer.DataBase.ConnectionString;
            var conectarDB = DatosLayer.DataBase.GetSqlConnection();
            */
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var cliente = customerRepository.OntenerPorID(txtBuscar.Text);
            tboxCustomerID.Text = cliente.CustomerID;
            tboxCompanyName.Text = cliente.CompanyName;
            tboxContactName.Text = cliente.ContactName;
            tboxContactTitle.Text = cliente.ContactTitle;
            tboxAdress.Text = cliente.Address;
            tboxCity.Text = cliente.City;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            var resultado = 0;
            var nuevoCliente = ObtenerNuevoCliente();
            if (validarCampoNull(nuevoCliente) == false)
            {
                resultado = customerRepository.InsertarCliente(nuevoCliente);
                MessageBox.Show("Registro Ingresado Correctamente");
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos por favor" + resultado);
            }
        }

        public Boolean validarCampoNull(Object objeto)
        {
            foreach (PropertyInfo property in objeto.GetType().GetProperties())
            {
                object value = property.GetValue(objeto, null);
                if ((String)value == "")
                {
                    return true;
                }
            }
            return false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var actualizarCliente = ObtenerNuevoCliente();
            int actualizadas = customerRepository.ActualizarCliente(actualizarCliente);
            MessageBox.Show($"Filas actualizadas = {actualizadas}");
        }

        private Customers ObtenerNuevoCliente()
        {
            var nuevoCliente = new Customers
            {
                CustomerID = tboxCustomerID.Text,
                CompanyName = tboxCompanyName.Text,
                ContactName = tboxContactName.Text,
                ContactTitle = tboxContactTitle.Text,
                Address = tboxAdress.Text,
                City = tboxCity.Text
            };

            return nuevoCliente;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int elimindas = customerRepository.EliminarCliente(tboxCustomerID.Text);
            MessageBox.Show("Filas eliminadas = " + elimindas);
        }
    }
}

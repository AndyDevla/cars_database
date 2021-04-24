using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// https://www.guru99.com/c-sharp-access-database.html
namespace carrosApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection cnn;
        private void Connect_Click(object sender, EventArgs e)
        {
            bool Conectar = true;

            if (cnn != null)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    Conectar = false;
                    MessageBox.Show("Connection is open");
                }
            }


            if (Conectar == true)
            {
                string connectionString; // se crea la variable de texto a la cual sera asignada la cadena de conexion a la base            
                connectionString = @"server=DESKTOP-5F3BR9R;database=cars;integrated security = true"; // se asigna la cadena de conexion a la variable
                cnn = new SqlConnection(connectionString); // clase SqlConnection se instancia como "cnn" para recibir la cadena de conexion

                cnn.Open(); // SqlConnection instanciado como "cnn" llama al metodo Open, abriendo la base de datos 
                MessageBox.Show("Connected Succesfully !!!");
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (cnn != null)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close(); // SqlConnection instanciado como "cnn" llama al metodo Open, cerrando la base de datos
                    MessageBox.Show("Disconnected Succesfully !!!");
                }
            }
            else
            {
                MessageBox.Show("Connection is closed");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command;
                SqlDataReader dataReader;
                string sql, Output = "";

                sql = "select carID, carBrand, carType, carSeries, carModel, carColor from brands";

                command = new SqlCommand(sql, cnn);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + " - " +
                                      dataReader.GetValue(3) + " - " + dataReader.GetValue(4) + " - " + dataReader.GetValue(5) + "\n";
                }

                label1.Text = Output;

                dataReader.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("showButtonError: "+ex.Message);
            }

        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace ProjectMySql
{
    public partial class CreateUser : Form
    {
        public CreateUser()
        {
            InitializeComponent();
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                String username_create = txtUsernameCreate.Text;
                String password_create = EncryptPassword(txtPasswordCreate.Text);

                // indicamos la consulta a realizar
                String insertarUsuario = "insert into usuarios(username,userpassword) values('" + username_create.ToString() + "','" + password_create.ToString() + "');";

                // generamos un string con los datos de conexion
                String informacion = "server=localhost;uid=diracspace;password=roberto33498;database=mysqlProject";

                //objetos tipo mysql para mandar informacion a base de datos
                MySqlConnection conexion = new MySqlConnection(informacion);

                // objeto para mandar consulta
                MySqlCommand consultas = new MySqlCommand(insertarUsuario, conexion);

                //objeto tipo mysql para obtener una sesion de lectura
                MySqlDataReader proceso;

                try
                {
                    // iniciando conexion 
                    conexion.Open();

                    //ejecuto mi consulta
                    proceso = consultas.ExecuteReader();

                    //muestro un cuadro de dialogo 
                    DialogResult respuesta = MessageBox.Show("¿Desea regresar al inicio de sesión?", "Pregunta", MessageBoxButtons.YesNo);

                    if (respuesta == DialogResult.Yes)
                    {
                        // borro los datos del texto
                        txtUsernameCreate.Clear();
                        txtPasswordCreate.Clear();
                    }

                    //cierro las conexiones de base de datos
                    conexion.Close();

                    //cierro el objeto de consultas
                    proceso.Close();

                    Login obj = new Login();
                    this.Hide();
                    obj.ShowDialog();
                    this.Close();
                }
                catch (Exception db)
                {
                    MessageBox.Show("¡Error en base de datos: "+db.Message+"!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Error en: " + ex.Message + "!");
            }
        }

        private String EncryptPassword(String input)
        {
            // metodo para encriptar
            String result = "";
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            result = Convert.ToBase64String(inArray);
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}

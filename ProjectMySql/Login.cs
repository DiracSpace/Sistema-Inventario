using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjectMySql
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // genero y almaceno los datos 
                String username = txtUsername.Text.ToString();
                String password = EncryptPassword(txtPassword.Text.ToString());

                // indicamos la consulta a realizar
                String checarUsuario = "select * from usuarios where username = '" + username.ToString() + "' and userpassword = '" + password.ToString() + "';";

                // generamos un string con los datos de conexion
                String informacion = "server=localhost;uid=diracspace;password=roberto33498;database=mysqlProject";

                //objetos tipo mysql para mandar informacion a base de datos
                MySqlConnection conexion = new MySqlConnection(informacion);

                // objeto para mandar consulta
                MySqlCommand consultas = new MySqlCommand(checarUsuario, conexion);

                //objeto tipo mysql para obtener una sesion de lectura
                MySqlDataReader proceso;

                try
                {
                    // iniciando conexion 
                    conexion.Open();

                    // ejecutar
                    proceso = consultas.ExecuteReader();

                    while (proceso.Read())
                    {
                        String temp_username = (proceso["username"].ToString());
                        String temp_password = (proceso["userpassword"].ToString());

                        if (username.Equals(temp_username) && password.Equals(temp_password))
                        {
                            try
                            {
                                var w = new Form() { Size = new Size(0, 0) };
                                Task.Delay(TimeSpan.FromSeconds(5))
                                    .ContinueWith((t) => w.Close(), TaskScheduler.FromCurrentSynchronizationContext());

                                MessageBox.Show(w, "Ingresando, espere ..", "Inicio sesión");
                                InterfazPrincipal obj = new InterfazPrincipal();
                                this.Hide();
                                obj.ShowDialog();
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error "+ex.Message+", volver a intentar");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No existe tal cuenta, cree primero su cuenta");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("¡Error en base de datos: " + ex.Message + "!");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("¡Error al inciar: " + ex.Message + "!");
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
        private void lblCreateUser_Click(object sender, EventArgs e)
        {
            CreateUser obj = new CreateUser();
            this.Hide();
            obj.ShowDialog();
            this.Close();
        }
    }
}

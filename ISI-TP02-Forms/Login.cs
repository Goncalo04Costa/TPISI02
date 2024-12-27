using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISI_TP02_Forms
{
    public partial class Login : Form
    {
        private HttpClient HttpClient = new HttpClient();

        public Login()
        {
            InitializeComponent();
            HttpClient.BaseAddress = new Uri("https://localhost:7275");
        }

     
        //Botão LOGIN
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FuncoesAuxiliares funcAux = new FuncoesAuxiliares();
                var loginRequest = new
                {
                    NIF = textBoxUtilizador.Text,
                    password = textBoxPassword.Text,
                };
                var jsonPayload = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpclient.PostAsync("api/Pessoa/RealizarLogin", jsonPayload);
                response.EnsureSuccessStatusCode();


                var responseContent = await response.Content.ReadAsStringAsync();
                jwtToken = responseContent.Trim('\"');

                loggedPessoa = funcAux.DecodeJwtToken(jwtToken);
                MessageBox.Show("Autenticado com sucesso.");
                if (loggedPessoa.Role == "Admin")
                {
                    Hide();
                    menuAdmin = new MenuAdmin(this, jwtToken);
                    menuAdmin.Show();
                }
                else if (loggedPessoa.Role == "Utilizador")
                {
                    Hide();
                    menuLogin = new MenuLogin(this, jwtToken);
                    menuLogin.Show();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }


        /// <summary>
        /// Para fechar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }
    }
}

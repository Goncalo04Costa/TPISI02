using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISI_TP02_Forms
{
    public partial class Login : Form
    {
        private Metodos metodos = new Metodos();
        private HttpClient httpClient = new HttpClient();
        public Login()
        {
            InitializeComponent();
            //httpClient.BaseAddress = new Uri("https://gestaohospitalar-eyere9hbc9ehgqdw.ukwest-01.azurewebsites.net/");
            httpClient.BaseAddress = new Uri("https://localhost:7275/");
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var newRequest = new LogRequest
                {
                    NIF = Convert.ToInt32(textBoxNIF.Text),
                    Password = textBoxPassword.Text,
                };
                var jsonPayload = new StringContent(JsonSerializer.Serialize(newRequest), Encoding.UTF8, "application/json");
                HttpResponseMessage requestLogin = await httpClient.PostAsync("api/Funcionario/RealizarLogin", jsonPayload);
                requestLogin.EnsureSuccessStatusCode();

                var tokenReceived = await requestLogin.Content.ReadAsStringAsync();
                Funcionario teste = metodos.DecodeJwtToken(tokenReceived);

                MessageBox.Show($"Bem-vindo(a), {teste.Nome}");

                // Abre o Forms1
                var forms1 = new Form1();
                forms1.Show();

                // Oculta o Form atual
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar login: {ex.Message}");
            }
        }

        private void textBoxNIF_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

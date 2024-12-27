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
    public partial class Form1 : Form
    {
        private HttpClient HttpClient = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            HttpClient.BaseAddress = new Uri("https://localhost:7275");
        }

        private async void button1_Click(object sender, EventArgs e) //OBTER FUNCIONARIO
        {
            HttpResponseMessage request = await HttpClient.GetAsync("api/Funcionario/obter");
            request.EnsureSuccessStatusCode();

            var requestContent = await request.Content.ReadAsStringAsync();
            List<Funcionario> list = JsonSerializer.Deserialize<List<Funcionario>>(requestContent);


            dataGridView1.DataSource = list;
        }

        private async void button2_Click_1(object sender, EventArgs e) //OBTER CONSULTAS
        {
            HttpResponseMessage request = await HttpClient.GetAsync("api/Consulta/obter");
            request.EnsureSuccessStatusCode();

            var requestContent = await request.Content.ReadAsStringAsync();
            List<Consulta> list = JsonSerializer.Deserialize<List<Consulta>>(requestContent);


            dataGridView1.DataSource = list;
        }

        private async void button3_Click_1(object sender, EventArgs e) //OBTER UTENTES
        {
            HttpResponseMessage request = await HttpClient.GetAsync("api/Utente/obter");
            request.EnsureSuccessStatusCode();

            var requestContent = await request.Content.ReadAsStringAsync();
            List<Utente> list = JsonSerializer.Deserialize<List<Utente>>(requestContent);


            dataGridView1.DataSource = list;
        }

        private void button4_Click(object sender, EventArgs e) //adicionar funcinario
        {
            // Cria uma instância de Form2
            Form2Funcionario form2 = new Form2Funcionario();

            // Exibe o Form2
            form2.Show();

            // Oculta o Form1 (se você não quiser fechá-lo completamente)
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Cria uma instância de Form2
            Form3Utente form3 = new Form3Utente();

            // Exibe o Form2
            form3.Show();

            // Oculta o Form1 (se você não quiser fechá-lo completamente)
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4Consulta form4Consulta = new Form4Consulta();

            form4Consulta.Show(); 
            
            this.Hide();


        }
    }
}

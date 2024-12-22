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

        private async void button2_Click(object sender, EventArgs e) //OBTER CONSULTAS
        {
            HttpResponseMessage request = await HttpClient.GetAsync("api/Consulta/obter");
            request.EnsureSuccessStatusCode();
        }

        private async void button3_Click(object sender, EventArgs e) //OBTER UTENTES
        {

        }

    }
}

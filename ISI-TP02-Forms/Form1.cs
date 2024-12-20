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

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpResponseMessage request = await HttpClient.GetAsync("api/Funcionario/obter");
            request.EnsureSuccessStatusCode();

            var requestContent = await request.Content.ReadAsStringAsync();
            List<Funcionario> list = JsonSerializer.Deserialize<List<Funcionario>>(requestContent);


            dataGridView1.DataSource = list;
        }
    }
}

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
    public partial class Form4Consulta : Form
    {
        private HttpClient HttpClient = new HttpClient();
        public Form4Consulta()
        {
            InitializeComponent();
            HttpClient.BaseAddress = new Uri("https://localhost:7275");
        }

        private void Form4Consulta_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cria uma instância de Form1
            Form1 form1 = new Form1();

            // Exibe Form1
            form1.Show();

            // Oculta Form3 (se você não quiser fechá-lo completamente)
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            Consulta novaConsulta = new Consulta
            {
                UtenteId = int.Parse(textBox1.Text),
                FuncionárioId = int.Parse(textBox2.Text),
                HospitalId = int.Parse(textBox3.Text),
                MedicoId = int.Parse(textBox4.Text),
                Data = DateTime.Parse(textBox5.Text),
                Descricao = textBox7.Text,

            };

            //Console.WriteLine(novaConsulta.TipoConsultaId);

            // Serializa o objeto Funcionario para JSON

            var json = JsonSerializer.Serialize(novaConsulta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Envia a requisição POST para a API
            HttpResponseMessage response = await HttpClient.PostAsync("api/Consulta/create", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Consulta criada com sucesso!");
            }
            else
            {
                MessageBox.Show("Falha ao criar a Consulta.");
            }
        }
    }
}
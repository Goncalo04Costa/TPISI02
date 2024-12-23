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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ISI_TP02_Forms
{
    public partial class Form3Utente : Form
    {

        private HttpClient HttpClient = new HttpClient();
        public Form3Utente()
        {
            InitializeComponent();
            HttpClient.BaseAddress = new Uri("https://localhost:7275");
        }

        private void Form3Utente_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {   
            Utente novoUtente = new Utente
            {
                Nome = textBox1.Text,
                NIF = int.Parse(textBox2.Text),
                DataEntrada = DateTime.Parse(textBox3.Text),
                TipoUtenteId = int.Parse(textBox4.Text),
                HospitalId = int.Parse(textBox5.Text)

            };

            //Console.WriteLine(novoUtente.TipoUtenteId);

            // Serializa o objeto Funcionario para JSON
            var json = JsonSerializer.Serialize(novoUtente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Envia a requisição POST para a API
            HttpResponseMessage response = await HttpClient.PostAsync("api/Utente/create", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Utente criado com sucesso!");
            }
            else
            {
                MessageBox.Show("Falha ao criar o Utente.");
            }
        }

        private void button2_Click(object sender, EventArgs e) //volta form1
        {
            // Cria uma instância de Form1
            Form1 form1 = new Form1();

            // Exibe Form1
            form1.Show();

            // Oculta Form3 (se você não quiser fechá-lo completamente)
            this.Hide();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

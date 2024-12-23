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
    public partial class Form2Funcionario : Form
    {
        private HttpClient HttpClient = new HttpClient();
        public Form2Funcionario()
        {
            InitializeComponent();
            HttpClient.BaseAddress = new Uri("https://localhost:7275");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e) //adiciona funcioanario
        {
            Funcionario novoFuncionario = new Funcionario
            {
                Nome = textBox1.Text,
                NIF = int.Parse(textBox2.Text),
                DataEntrada = DateTime.Parse(textBox3.Text),
                Contacto = int.Parse(textBox4.Text),
                Password = textBox5.Text,
                TipoFuncionárioId = int.Parse(textBox6.Text)
            };

            Console.WriteLine(novoFuncionario.TipoFuncionárioId);

            // Serializa o objeto Funcionario para JSON
            var json = JsonSerializer.Serialize(novoFuncionario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Envia a requisição POST para a API
            HttpResponseMessage response = await HttpClient.PostAsync("api/Funcionario/create", content);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Funcionário criado com sucesso!");
            }
            else
            {
                MessageBox.Show("Falha ao criar o funcionário.");
            }
        }

        private void button2_Click(object sender, EventArgs e) // voltar fomr1
        {
            // Cria uma instância de Form1
            Form1 form1 = new Form1();

            // Exibe Form1
            form1.Show();

            // Oculta Form2 (se você não quiser fechá-lo completamente)
            this.Hide();
        }
    }
}

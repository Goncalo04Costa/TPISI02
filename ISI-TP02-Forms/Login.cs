using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISI_TP02_Forms.ServiceReference2;// Ajuste o namespace conforme necessário

namespace ISI_TP02_Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HospitalServiceClient())
                {
                    var funcionario = new ServiceReference2.Funcionario
                    {
                        NIF = int.Parse(textBox1.Text),
                        Password = textBox2.Text
                    };

                    ServiceReference2.Funcionario resultado = await client.AutenticarUtilizadorAsync(funcionario);

                    if (resultado != null)
                    {
                        MessageBox.Show($"Login bem-sucedido! Bem-vindo, {resultado.Nome}");

                        // Instanciar o Form1
                        Form1 form1 = new Form1();

                        // Exibir o Form1
                        form1.Show();

                        // Ocultar o formulário de login (opcional: use Close para fechar completamente)
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("Credenciais inválidas.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }


    }
}

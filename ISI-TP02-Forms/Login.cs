using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    var funcionario = new Funcionario
                    {
                        NIF = int.Parse(textBox1.Text),
                        Password = textBox2.Text
                    };

                    Funcionario resultado = await client.AutenticarUtilizadorAsync(funcionario);

                    if (resultado != null)
                    {
                        MessageBox.Show($"Login bem-sucedido! Bem-vindo, {resultado.Nome}");
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

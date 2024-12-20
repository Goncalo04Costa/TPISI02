using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISI_TP02_Forms
{
    public partial class Form1 : Form
    {
        private HttpClient HttpClient;
        public Form1()
        {
            InitializeComponent();
            HttpClient.BaseAddress = new Uri("https://localhost:7275");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpResponseMessage request = HttpClient.GetAsync("")
        }
    }
}

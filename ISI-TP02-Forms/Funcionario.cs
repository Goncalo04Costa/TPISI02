using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ISI_TP02_Forms
{
    internal class Funcionario
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]

        public string Nome { get; set; }
        [JsonPropertyName("nif")]

        public int NIF { get; set; }
        [JsonPropertyName("dataEntrada")]

        public DateTime DataEntrada { get; set; }
        [JsonPropertyName("contacto")]

        public int Contacto { get; set; }
        [JsonPropertyName("password")]

        public string Password { get; set; }
        [JsonPropertyName("tipoFuncionárioid")]

        public int TipoFuncionárioid { get; set; }
    }
}


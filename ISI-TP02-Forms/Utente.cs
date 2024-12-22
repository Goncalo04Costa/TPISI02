using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISI_TP02_Forms
{
    internal class Utente
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }

        [JsonPropertyName("nif")]
        public int NIF { get; set; }

        [JsonPropertyName("dataEntrada")]
        public DateTime DataEntrada { get; set; }

        [JsonPropertyName("tipoUtente")]
        public int TipoUtente { get; set; }

        [JsonPropertyName("hospitalId")]
        public int HospitalId { get; set; }

    }
}

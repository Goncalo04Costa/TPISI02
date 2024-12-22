using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISI_TP02_Forms
{
    internal class Medico
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("tipoMedicoId")]
        public int TipoMedicoId { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISI_TP02_Forms
{
    internal class Consulta
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("utenteId")]

        public int UtenteId { get; set; }
        [JsonPropertyName("funcionarioId")]

        public int FuncionarioId { get; set; }
        [JsonPropertyName("medicoId")]

        public int MedicoId { get; set; }
        [JsonPropertyName("data")]

        public DateTime Data { get; set; }
        [JsonPropertyName("hora")]

        public DateTime Hora { get; set; }
        [JsonPropertyName("descricao")]

        public string Descricao { get; set; }
    }
}
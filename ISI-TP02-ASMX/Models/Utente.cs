using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Utente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NIF { get; set; }
        public DateTime DataEntrada { get; set; }
        public int TipoUtenteId { get; set; }
        public int HospitalId { get; set; }
    }
}


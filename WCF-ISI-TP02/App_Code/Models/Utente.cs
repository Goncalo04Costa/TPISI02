using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{

    public class Utente
    {
        public int Id { get; set; } // Primary Key
        public string Nome { get; set; }
        public int NIF { get; set; } // Unique
        public DateTime DataEntrada { get; set; }
        public TipoUtente TipoUtente { get; set; } // Navigation Property
        public Hospital Hospital { get; set; } // Navigation Property

        public List<Consulta> Consultas { get; set; } // Navigation Property
    }

}

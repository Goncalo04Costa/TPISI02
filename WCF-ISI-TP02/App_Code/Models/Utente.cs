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
        public int TipoUtenteId { get; set; } // Foreign Key
        public TipoUtente TipoUtente { get; set; } // Navigation Property
        public int HospitalId { get; set; } // Foreign Key
        public Hospital Hospital { get; set; } // Navigation Property

        public ICollection<Consulta> Consultas { get; set; } // Navigation Property
    }

}

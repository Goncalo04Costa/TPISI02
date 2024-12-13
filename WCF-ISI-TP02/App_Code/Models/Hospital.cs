using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Hospital
    {
        public int Id { get; set; } // Primary Key
        public string Nome { get; set; }
        public string Localizacao { get; set; }

        public ICollection<Utente> Utentes { get; set; } // Navigation Property
        public ICollection<Consulta> Consultas { get; set; } // Navigation Property
    }
}

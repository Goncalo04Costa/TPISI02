using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class TipoUtente
    {
        public int Id { get; set; } // Primary Key
        public string Descricao { get; set; }

        public List<Utente> Utentes { get; set; } // Navigation Property
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class TipoMedico
    {
        public int Id { get; set; } // Primary Key
        public string Descricao { get; set; }

        public List<Medico> Medicos { get; set; } // Navigation Property
    }
}

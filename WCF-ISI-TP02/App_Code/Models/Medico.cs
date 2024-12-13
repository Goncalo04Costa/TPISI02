using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Medico
    {
        public int Id { get; set; } // Primary Key
        public string Nome { get; set; }
        public int TipoMedicoId { get; set; } // Foreign Key
        public TipoMedico TipoMedico { get; set; } // Navigation Property

        public ICollection<Consulta> Consultas { get; set; } // Navigation Property
    }
}

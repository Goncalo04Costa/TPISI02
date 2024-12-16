using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Medico
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public TipoMedico TipoMedico { get; set; } 

        public List<Consulta> Consultas { get; set; }
    }
}

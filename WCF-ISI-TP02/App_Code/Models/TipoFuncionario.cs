using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class TipoFuncionario
    {
        public int Id { get; set; } // Primary Key
        public string Descricao { get; set; }

        public ICollection<Funcionario> Funcionarios { get; set; } // Navigation Property
    }
}

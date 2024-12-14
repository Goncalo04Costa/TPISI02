using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Localizacao { get; set; }
    }
}

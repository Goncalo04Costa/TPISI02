using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NIF { get; set; }
        public DateTime DataEntrada { get; set; }
        public int Contacto { get; set; }
        public string Password { get; set; }
        public int TipoFuncionárioid { get; set; }
        public string JWT { get; set; }
    }
}


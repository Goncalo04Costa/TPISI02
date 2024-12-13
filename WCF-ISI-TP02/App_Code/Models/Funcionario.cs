﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISITP02.Models
{
    public class Funcionario
    {
        public int Id { get; set; } // Primary Key
        public string Nome { get; set; }
        public int NIF { get; set; } // Unique
        public DateTime DataEntrada { get; set; }
        public int Contacto { get; set; }
        public string Password { get; set; } // Unique
        public int TipoFuncionarioId { get; set; } // Foreign Key
        public TipoFuncionario TipoFuncionario { get; set; } // Navigation Property
        public string JWT { get; set; }

        public ICollection<Consulta> Consultas { get; set; } // Navigation Property
    }


}

    using System;
    using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

    namespace ISITP02.Models
    {
        public class Consulta
        {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public Utente Utente { get; set; }
        public int FuncionárioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
    }
    }

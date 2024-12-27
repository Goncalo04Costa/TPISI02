using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ISITP02.Models
{
    public class Consulta
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("UtenteId")]
        public int UtenteId { get; set; }

        [XmlElement("FuncionárioId")]
        public int FuncionárioId { get; set; }

        [XmlElement("HospitalId")]
        public int HospitalId { get; set; }

        [XmlElement("MedicoId")]
        public int MedicoId { get; set; }

        [XmlElement("Data")]
        public DateTime Data { get; set; }


        [XmlElement("Descricao")]
        public string Descricao { get; set; }
    }
}

using ISITP02.Models;

namespace ISI_TP02_APIRestful.Models
{
    public class RegistarConsultaReq
    {
        public int UtenteId { get; set; }
        public Utente Utente { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Descricao { get; set; }
    }
}


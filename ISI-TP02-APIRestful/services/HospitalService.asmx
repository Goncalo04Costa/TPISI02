using System;
using System.Collections.Generic;
using System.Web.Services;

namespace ISI_TP02_APIRestful.services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class HospitalService : WebService
    {
        // Dummy data to simulate database for Funcionario
        private static readonly List<Funcionario> funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = 1, Nome = "Carlos Silva", NIF = 123456789, DataEntrada = DateTime.Now.AddYears(-5), Contacto = 912345678, TipoFuncionarioId = 1 },
            new Funcionario { Id = 2, Nome = "Ana Pereira", NIF = 987654321, DataEntrada = DateTime.Now.AddYears(-3), Contacto = 913456789, TipoFuncionarioId = 2 }
        };

        // SOAP Method to Get All Funcionarios
        [WebMethod]
        public List<Funcionario> GetAllFuncionarios()
        {
            return funcionarios;
        }

        // SOAP Method to Get Funcionario by ID
        [WebMethod]
        public Funcionario GetFuncionarioById(int id)
        {
            return funcionarios.Find(f => f.Id == id);
        }

        // SOAP Method to Add a New Funcionario
        [WebMethod]
        public string CreateFuncionario(string nome, int nif, DateTime dataEntrada, int contacto, int tipoFuncionarioId)
        {
            int newId = funcionarios.Count + 1;
            funcionarios.Add(new Funcionario
            {
                Id = newId,
                Nome = nome,
                NIF = nif,
                DataEntrada = dataEntrada,
                Contacto = contacto,
                TipoFuncionarioId = tipoFuncionarioId
            });
            return $"Funcionario '{nome}' added successfully with ID {newId}.";
        }

        // SOAP Method to Delete a Funcionario by ID
        [WebMethod]
        public string DeleteFuncionario(int id)
        {
            var funcionario = funcionarios.Find(f => f.Id == id);
            if (funcionario == null)
            {
                return $"Funcionario with ID {id} not found.";
            }

            funcionarios.Remove(funcionario);
            return $"Funcionario with ID {id} removed successfully.";
        }
    }

    // Funcionario class definition
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NIF { get; set; }
        public DateTime DataEntrada { get; set; }
        public int Contacto { get; set; }
        public int TipoFuncionarioId { get; set; }
    }
}

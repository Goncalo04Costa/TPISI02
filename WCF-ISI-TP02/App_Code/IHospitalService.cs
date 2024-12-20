using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

[ServiceContract]
public interface IHospitalService
{

    [OperationContract]
    bool CreateMedico(string nome, int tipoMedicoId);

    [OperationContract]
    List<Medico> GetAllMedicos();

    [OperationContract]
    Medico GetMedicoById(int id);

    [OperationContract]
    bool UpdateMedico(int id, string nome, int tipoMedicoId);

    [OperationContract]
    bool DeleteMedico(int id);

    [OperationContract]
    bool CreateTipoMedico(string descricao);

    [OperationContract]
    bool DeleteTipoMedico(string descricao);

    [OperationContract]
    bool CreateHospital(string nome, string localizacao);

    [OperationContract]
    List<Hospital> GETALLHOSPITAIS();

    [OperationContract]
    Hospital GetHospitalByLoc(string localização);

    [OperationContract]
     Task<Funcionario> AutenticarUtilizador(Funcionario f);

}
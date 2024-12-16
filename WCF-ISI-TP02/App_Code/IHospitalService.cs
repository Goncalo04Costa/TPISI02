using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

[ServiceContract]
public interface IHospitalService
{
    // Funcionario Methods
    [OperationContract]
    List<Funcionario> GetAllFuncionarios();
    [OperationContract]
    Funcionario GetFuncionarioById(int id);

    // Utente Methods
    [OperationContract]
    List<Utente> GetAllUtentes();
    [OperationContract]
    List<Utente> GetUtentesById();

    // Medico Methods
    [OperationContract]
    List<Medico> GetAllMedicos();
    [OperationContract]
    List<Medico> GetMedicoById();

}

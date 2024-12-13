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
    [OperationContract]
    bool CreateFuncionario(string nome, int nif, DateTime dataEntrada, int contacto, string password, int tipoFuncionarioId, string jwt);
    [OperationContract]
    void UpdateFuncionario(int id, string nome, int nif, DateTime dataEntrada, int contacto, string password, int tipoFuncionarioId, string jwt);
    [OperationContract]
    bool DeleteFuncionario(int id);

    // Utente Methods
    [OperationContract]
    List<Utente> GetAllUtentes();
    [OperationContract]
    bool CreateUtente(string nome, int nif, DateTime dataEntrada, int tipoUtenteId, int hospitalId);
    //bool UpdateUtente(int id, string nome, int nif, DateTime dataEntrada, int tipoUtenteId, int hospitalId);
    [OperationContract]
    bool DeleteUtente(int id);

    // Medico Methods
    [OperationContract]
    List<Medico> GetAllMedicos();
    [OperationContract]
    bool CreateMedico(string nome, int tipoMedicoId);
    [OperationContract]
    bool DeleteMedico(int id);
}

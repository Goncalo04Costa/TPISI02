using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

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
    bool createConsulta(int UtenteId, int FuncionarioId, int hospitalId,int medicoId, DateTime data, TimeSpan hora, string descricao);

    [OperationContract]
    List<Consulta> GetAllConsultas();

    [OperationContract]
    bool deleteConsulta(int id);

    [OperationContract]
    bool UpdateConsulta(int UtenteId, int FuncionarioId, int hospitalId, int medicoId, DateTime data, TimeSpan hora, string descricao);

    [OperationContract]
    Consulta ConsultaById(int id);  //metodo retorna apenas um objeto 

    [OperationContract]
    List<Consulta> ConsultaByHospital(int hospitalId); //retorna multiplos resultados -hospital-varias consultas

    [OperationContract]
    List<Consulta> ConsultaByUtente(int utenteId);

    [OperationContract]
    List<Consulta> ConsultaByFuncionario(int funcionarioId);



}
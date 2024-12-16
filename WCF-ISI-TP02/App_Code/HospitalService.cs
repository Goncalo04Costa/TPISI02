using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Services;

public class HospitalService : IHospitalService
{
    private string connectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public HospitalService() { }

    [WebMethod]
    public bool CreateMedico(string nome, int tipoMedicoId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Medico (Nome, TipoMedicoId) VALUES (@Nome, @TipoMedicoId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@TipoMedicoId", tipoMedicoId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error creating medico", ex);
        }
    }

    [WebMethod]
    public List<Medico> GetAllMedicos()
    {
        List<Medico> medicos = new List<Medico>();
        List<TipoMedico> tiposMedico = new List<TipoMedico>();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome, TipoMedicoId FROM Medico ";
                string obterQuery = "SELECT id, descricao FROM TipoMedico";
                using (SqlCommand obterTipoMedico = new SqlCommand(obterQuery, connection))
                {
                    using (SqlDataReader reader = obterTipoMedico.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposMedico.Add(new TipoMedico
                            {
                                Id = reader.GetInt32(0),
                                Descricao = reader.GetString(1)
                            });
                        }
                    }
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int tipoMedicoId = reader.GetInt32(2);
                            TipoMedico tipoMedico = tiposMedico.FirstOrDefault(t => t.Id == tipoMedicoId);
                            medicos.Add(new Medico
                           {
                               Id = reader.GetInt32(0),
                               Nome = reader.GetString(1),
                               TipoMedico = tipoMedico
                           });
                        }
                    }
                }
            }
            return medicos;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error fetching medicos", ex);
        }
    }

    [WebMethod]
    public Medico GetMedicoById(int id)
    {
        List<TipoMedico> tiposMedico = new List<TipoMedico>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nome, TipoMedicoId FROM Medico WHERE Id = @Id";
                string obterQuery = "SELECT id, descricao FROM TipoMedico";
                using (SqlCommand obterTipoMedico = new SqlCommand(obterQuery, connection))
                {
                    using (SqlDataReader reader = obterTipoMedico.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tiposMedico.Add(new TipoMedico
                            {
                                Id = reader.GetInt32(0),
                                Descricao = reader.GetString(1)
                            });
                        }
                    }
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int tipoMedicoId = reader.GetInt32(2);
                            TipoMedico tipoMedico = tiposMedico.FirstOrDefault(t => t.Id == tipoMedicoId);

                            return new Medico
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                TipoMedico = tipoMedico
                            };
                        }
                    }
                }
            }
            return null; 
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error fetching medico by ID", ex);
        }
    }


    [WebMethod]
    public bool UpdateMedico(int id, string nome, int tipoMedicoId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Medico SET Nome = @Nome, TipoMedicoId = @TipoMedicoId WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@TipoMedicoId", tipoMedicoId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error updating medico", ex);
        }
    }


    [WebMethod]
    public bool DeleteMedico(int id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Medico WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error deleting medico", ex);
        }
    }


    [WebMethod]
    public bool CreateTipoMedico(string descricao)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO TipoMedico (Descricao) VALUES (@Descricao)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", descricao);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error creating TipoMedico", ex);
        }
    }

    [WebMethod]
    public bool DeleteTipoMedico(string descricao)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM TipoMedico (Descricao) VALUES (@Descricao)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descricao", descricao);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error creating TipoMedico", ex);
        }
    }

    [WebMethod]
    public bool CreateHospital(string nome, string localizacao)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Hospital (nome, localizacao) VALUES (@nome, @localizacao)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@localizacao", localizacao);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error creating hospital", ex);
        }
    }

    [WebMethod]
    public List<Hospital> GETALLHOSPITAIS()
    {
        List<Hospital> hospitais = new List<Hospital>();
        List<Consulta> consultas = new List<Consulta>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string query = "SELECT id, nome, localizacao FROM Hospital";
                string obterQuery = "SELECT id, data, Hospitalid FROM Consulta";

                
                using (SqlCommand obterConsultas = new SqlCommand(obterQuery, connection))
                {
                    using (SqlDataReader reader = obterConsultas.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            consultas.Add(new Consulta
                            {
                                Id = reader.GetInt32(0),        
                                Data = reader.GetDateTime(1),     
                                HospitalId = reader.GetInt32(2)   
                            });
                        }
                    }
                }

                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int hospitalId = reader.GetInt32(0);

                            
                            List<Consulta> hospitalConsultas = consultas.Where(c => c.HospitalId == hospitalId).ToList();

                            hospitais.Add(new Hospital
                            {
                                Id = hospitalId,
                                Nome = reader.GetString(1),           
                                Localizacao = reader.GetString(2),   
                                Consultas = hospitalConsultas        
                            });
                        }
                    }
                }
            }
            return hospitais;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error fetching hospitais", ex);
        }
    }


    [WebMethod]
    public Hospital GetHospitalByLoc(string localização)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, nome, localizacao FROM Hospital WHERE localizacao = @localizacao";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@localizacao", localização);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Hospital
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Localizacao = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return null; 
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error fetching hospital by ID", ex);
        }
    }

}



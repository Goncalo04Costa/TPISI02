using ISITP02.Models;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Services;

public class HospitalService : IHospitalService
{
    private string connectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


    public HospitalService()
    {

    }


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



    [WebMethod]
    public bool CreateConsulta(int id, int utenteId, int funcionarioId, int hospitalId, int medicoId,
                           DateTime data, TimeSpan hora, string descricao)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Consulta (id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data, hora, descricao) 
                             VALUES (@id, @Utenteid, @Funcionárioid, @Hospitalid, @Medicoid, @data, @hora, @descricao)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@Utenteid", utenteId);
                    command.Parameters.AddWithValue("@FuncionarioId", (object)funcionarioId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Hospitalid", hospitalId);
                    command.Parameters.AddWithValue("@Medicoid", medicoId);
                    command.Parameters.AddWithValue("@data", data);
                    command.Parameters.AddWithValue("@hora", hora);
                    command.Parameters.AddWithValue("@descricao", descricao);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Retorna true se a inserção foi bem-sucedida
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao criar consulta", ex);
        }
    }


    [WebMethod]

    public List<Consulta> GetAllConsultas()
    {
        List<Consulta> consultas = new List<Consulta>();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Consulta";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        consultas.Add(new Consulta
                        {
                            Id = (int)reader["id"],
                            UtenteId = (int)reader["Utenteid"],
                            FuncionarioId = (int)reader["Funcionárioid"],
                            HospitalId = (int)reader["Hospitalid"],
                            MedicoId = (int)reader["Medicoid"],
                            Data = (DateTime)reader["data"],
                            Hora = (TimeSpan)reader["hora"],
                            Descricao = reader["descricao"].ToString()
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro a pesquisar consultas", ex);
        }
        return consultas;
    }

    [WebMethod]

    public bool deleteConsulta(int id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Consulta WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao eliminar consulta", ex);
        }
    }


    [WebMethod]
    public bool UpdateConsulta(int UtenteId, int FuncionarioId, int hospitalId, int medicoId, DateTime data, TimeSpan hora, string descricao)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Consulta SET FuncionarioId = @FuncionarioId, HospitalId = @HospitalId, MedicoId = @MedicoId, Data = @Data, Hora = @Hora, Descricao = @Descricao WHERE UtenteId = @UtenteId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UtenteId", UtenteId);
                    command.Parameters.AddWithValue("@FuncionarioId", (object)FuncionarioId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@HospitalId", hospitalId);
                    command.Parameters.AddWithValue("@MedicoId", medicoId);
                    command.Parameters.AddWithValue("@Data", data);
                    command.Parameters.AddWithValue("@Hora", hora);
                    command.Parameters.AddWithValue("@Descricao", descricao);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao atualizar consulta", ex);
        }
    }


    [WebMethod]
    public List<Consulta> ConsultaByHospital(int hospitalId)
    {
        List<Consulta> consultas = new List<Consulta>();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Consulta WHERE Hospitalid = @Hospitalid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Hospitalid", hospitalId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            consultas.Add(new Consulta
                            {
                                Id = (int)reader["id"],
                                UtenteId = (int)reader["Utenteid"],
                                FuncionarioId = (int)reader["Funcionárioid"],
                                HospitalId = (int)reader["Hospitalid"],
                                MedicoId = (int)reader["Medicoid"],
                                Data = (DateTime)reader["data"],
                                Hora = (TimeSpan)reader["hora"],
                                Descricao = reader["descricao"].ToString()
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao procurar consultas por hospital", ex);
        }
        return consultas;
    }



    [WebMethod]
    public List<Consulta> ConsultaByUtente(int utenteId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data, hora, descricao 
                             FROM Consulta 
                             WHERE Utenteid = @Utenteid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Utenteid", utenteId);

                    List<Consulta> consultas = new List<Consulta>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            consultas.Add(new Consulta
                            {
                                Id = reader.GetInt32(0),               // id
                                UtenteId = reader.GetInt32(1),        // Utenteid
                                FuncionarioId = reader.GetInt32(2),   // Funcionárioid
                                HospitalId = reader.GetInt32(3),      // Hospitalid
                                MedicoId = reader.GetInt32(4),        // Medicoid
                                Data = reader.GetDateTime(5),         // data
                                Hora = reader.GetTimeSpan(6),         // hora
                                Descricao = reader.GetString(7)       // descricao
                            });
                        }
                    }
                    return consultas;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar consultas pelo utente", ex);
        }
    }

    [WebMethod]

    public List<Consulta> ConsultaByFuncionario(int funcionarioId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data, hora, descricao 
                             FROM Consulta 
                             WHERE Funcionárioid = @Funcionárioid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Funcionárioid", funcionarioId);

                    List<Consulta> consultas = new List<Consulta>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            consultas.Add(new Consulta
                            {
                                Id = reader.GetInt32(0),
                                UtenteId = reader.GetInt32(1),
                                FuncionarioId = reader.GetInt32(2),
                                HospitalId = reader.GetInt32(3),
                                MedicoId = reader.GetInt32(4),
                                Data = reader.GetDateTime(5),
                                Hora = reader.GetTimeSpan(6),
                                Descricao = reader.GetString(7)
                            });
                        }
                    }
                    return consultas;
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar consultas pelo funcionário", ex);
        }
    }


    [WebMethod]
    public Consulta ConsultaById(int id)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data, hora, descricao 
                             FROM Consulta 
                             WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Consulta
                            {
                                Id = reader.GetInt32(0),
                                UtenteId = reader.GetInt32(1),
                                FuncionarioId = reader.GetInt32(2),
                                HospitalId = reader.GetInt32(3),
                                MedicoId = reader.GetInt32(4),
                                Data = reader.GetDateTime(5),
                                Hora = reader.GetTimeSpan(6),
                                Descricao = reader.GetString(7)
                            };
                        }
                    }
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar consulta por ID", ex);
        }
    }

    [WebMethod]
    public List<Consulta> GetConsultasSemFuncionario()
    {
        List<Consulta> consultas = new List<Consulta>();
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data, hora, descricao 
                             FROM Consulta 
                             WHERE Funcionárioid IS NULL";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        consultas.Add(new Consulta
                        {
                            Id = (int)reader["id"],
                            UtenteId = (int)reader["Utenteid"],
                            FuncionarioId = null,
                            HospitalId = (int)reader["Hospitalid"],
                            MedicoId = (int)reader["Medicoid"],
                            Data = (DateTime)reader["data"],
                            Hora = (TimeSpan)reader["hora"],
                            Descricao = reader["descricao"].ToString()
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Erro ao buscar consultas sem funcionário", ex);
        }
        return consultas;
    }


    /// <summary>
    /// Recebe o objeto do utilizador e verifica as credenciais.
    /// </summary>
    /// <param name="f">O funcionário com NIF e senha para autenticação.</param>
    /// <returns>O funcionário autenticado ou null se as credenciais forem inválidas.</returns>
    [WebMethod]
    public async Task<Funcionario> AutenticarUtilizador(Funcionario f)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                await  connection.OpenAsync();
                // Query para buscar funcionário pelo NIF
                string query = @" SELECT Id, Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionárioid
                                 FROM Funcionário WHERE NIF = @NIF";

                SqlCommand verificarFuncionario = new SqlCommand(query, connection);
                verificarFuncionario.Parameters.AddWithValue("@NIF", f.NIF);


                TipoFuncionario T = new TipoFuncionario();
                f.TipoFuncionario = T;
                using (var reader = await verificarFuncionario.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                   
                        string passwordBd = reader.GetString(reader.GetOrdinal("Password"));

                        if (f.Password == passwordBd)
                        {
                            f.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            f.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                            f.NIF = reader.GetInt32(reader.GetOrdinal("NIF"));
                            f.DataEntrada = reader.GetDateTime(reader.GetOrdinal("DataEntrada"));
                            f.Contacto = reader.GetInt32(reader.GetOrdinal("Contacto"));
                            f.TipoFuncionario.Id = reader.GetInt32(reader.GetOrdinal("TipoFuncionárioid"));
                            return f; 
                        }
                    }
                }
                return null; 
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}





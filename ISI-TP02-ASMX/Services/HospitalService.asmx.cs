using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ISI_TP02_ASMX
{
    /// <summary>
    /// Descrição resumida de HospitalService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class HospitalService : System.Web.Services.WebService
    {

        private readonly string connectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private SqlConnection db = new SqlConnection();
        public HospitalService()
        {
            db.ConnectionString = connectionString;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        [WebMethod]
        public List<Funcionario> GetAllFuncionarios()
        {
            var funcionarios = new List<Funcionario>();
            string query = "SELECT f.Id, f.Nome, f.NIF, f.DataEntrada, f.Contacto, f.Password, " +
                           "f.TipoFuncionárioid, t.Descricao AS TipoFuncionarioDescricao " +
                           "FROM Funcionário f " +
                           "INNER JOIN TipoFuncionário t ON f.TipoFuncionárioid = t.Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            funcionarios.Add(new Funcionario
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                NIF = reader.GetInt32(2),
                                DataEntrada = reader.GetDateTime(3),
                                Contacto = reader.GetInt32(4),
                                Password = reader.GetString(5),
                                TipoFuncionárioid = reader.GetInt32(6),
                            });
                        }
                    }
                }
                return funcionarios;
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException($"Database error: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Funcionário data.", ex);
            }
        }

        [WebMethod]
        public Funcionario GetFuncionarioById(int id)
        {
            Funcionario funcionario = null;
            string query = "SELECT Id, Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionárioid " +
                           "FROM Funcionário WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                funcionario = new Funcionario
                                {
                                    Id = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    NIF = reader.GetInt32(2),
                                    DataEntrada = reader.GetDateTime(3),
                                    Contacto = reader.GetInt32(4),
                                    Password = reader.GetString(5),
                                    TipoFuncionárioid = reader.GetInt32(6),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Funcionário data.", ex);
            }
            return funcionario;
        }

        [WebMethod]
        public bool CreateFuncionario(Funcionario funcionario)
        {
            string query = "INSERT INTO Funcionário (Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionárioid ) " +
                           "VALUES (@Nome, @NIF, @DataEntrada, @Contacto, @Password, @TipoFuncionárioid)";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = funcionario.Nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = funcionario.NIF;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = funcionario.DataEntrada;
                        command.Parameters.Add("@Contacto", System.Data.SqlDbType.Int).Value = funcionario.Contacto;
                        command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = funcionario.Password;
                        command.Parameters.Add("@TipoFuncionárioid", System.Data.SqlDbType.Int).Value = funcionario.TipoFuncionárioid;
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating Funcionário.", ex);
            }
        }

        [WebMethod]
        public bool UpdateFuncionario(Funcionario funcionario)
        {
            string query = "UPDATE Funcionário SET Nome = @Nome, NIF = @NIF, DataEntrada = @DataEntrada, " +
                           "Contacto = @Contacto, Password = @Password, TipoFuncionárioid = @TipoFuncionárioid WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value=funcionario.Id;
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = funcionario.Nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = funcionario.NIF;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = funcionario.DataEntrada;
                        command.Parameters.Add("@Contacto", System.Data.SqlDbType.Int).Value = funcionario.Contacto;
                        command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = funcionario.Password;
                        command.Parameters.Add("@TipoFuncionárioid", System.Data.SqlDbType.Int).Value = funcionario.TipoFuncionárioid;
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating Funcionário.", ex);
            }
        }

        [WebMethod]
        public bool DeleteFuncionario(int id)
        {
            string query = "DELETE FROM Funcionário WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting Funcionário.", ex);
            }
        }


        [WebMethod]
        public List<Utente> GetAllUtentes()
        {
            var utentes = new List<Utente>();
            string query = "SELECT Id, Nome, NIF, DataEntrada, TipoUtenteId, HospitalId FROM Utente";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            utentes.Add(new Utente
                            {
                                Id = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                NIF = reader.GetInt32(2),
                                DataEntrada = reader.GetDateTime(3),
                                TipoUtenteId = reader.GetInt32(4),
                                HospitalId = reader.GetInt32(5)
                            });
                        }
                    }
                }
                return utentes;
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException($"Database error: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Utente data.", ex);
            }
        }

        [WebMethod]
        public Utente GetUtenteById(int id)
        {
            Utente utente = null;
            string query = "SELECT Id, Nome, NIF, DataEntrada, TipoUtenteId, HospitalId FROM Utente WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                utente = new Utente
                                {
                                    Id = reader.GetInt32(0),
                                    Nome = reader.GetString(1),
                                    NIF = reader.GetInt32(2),
                                    DataEntrada = reader.GetDateTime(3),
                                    TipoUtenteId = reader.GetInt32(4),
                                    HospitalId = reader.GetInt32(5)
                                };
                            }
                        }
                    }
                }
                return utente;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving Utente data.", ex);
            }
        }

        [WebMethod]
        public bool CreateUtente(string nome, int NIF, DateTime dataEntrada, int tipoUtenteId, int hospitalId)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome não pode ser nulo ou vazio.", nameof(nome));
            if (NIF <= 0)
                throw new ArgumentException("O NIF deve ser um valor positivo.", nameof(NIF));

            string query = "INSERT INTO Utente (Nome, NIF, DataEntrada, TipoUtenteId, HospitalId) " +
                           "VALUES (@Nome, @NIF, @DataEntrada, @TipoUtenteId, @HospitalId)";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = NIF;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = dataEntrada;
                        command.Parameters.Add("@TipoUtenteId", System.Data.SqlDbType.Int).Value = tipoUtenteId;
                        command.Parameters.Add("@HospitalId", System.Data.SqlDbType.Int).Value = hospitalId;

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
             
                throw new ApplicationException("Error creating Utente.", ex);
            }
        }


        [WebMethod]
        public bool UpdateUtente(int id, string nome, int nif, DateTime dataEntrada, int tipoUtenteId, int hospitalId)
        {
            string query = "UPDATE Utente SET Nome = @Nome, NIF = @NIF, DataEntrada = @DataEntrada, " +
                           "TipoUtenteId = @TipoUtenteId, HospitalId = @HospitalId WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Adicionando parâmetros
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = nif;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = dataEntrada;
                        command.Parameters.Add("@TipoUtenteId", System.Data.SqlDbType.Int).Value = tipoUtenteId;
                        command.Parameters.Add("@HospitalId", System.Data.SqlDbType.Int).Value = hospitalId;
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                        // Executa a query e verifica se linhas foram afetadas
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Retorna true se pelo menos uma linha foi alterada
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Lança uma exceção para log ou tratamento
                throw new ApplicationException("Error updating Utente.", ex);
            }
        }

        [WebMethod]
        public bool DeleteUtente(int id)
        {
            string query = "DELETE FROM Utente WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting Utente.", ex);
            }
        }
        [WebMethod]
        public bool CreateConsulta(int utenteId, int funcionarioId, int hospitalId, int medicoId, DateTime data, string descricao)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"INSERT INTO Consulta (Utenteid, Funcionárioid, Hospitalid, Medicoid, data, descricao) 
                                 VALUES (@Utenteid, @Funcionárioid, @Hospitalid, @Medicoid, @data, @descricao)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Utenteid", utenteId);
                        command.Parameters.AddWithValue("@Funcionárioid", funcionarioId);
                        command.Parameters.AddWithValue("@Hospitalid", hospitalId);
                        command.Parameters.AddWithValue("@Medicoid", medicoId);
                        command.Parameters.AddWithValue("@data", data);
                        command.Parameters.AddWithValue("@descricao", descricao);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
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
                    string query = "SELECT Id, Utenteid, Funcionárioid, Hospitalid, Medicoid, Data, Descricao FROM Consulta";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        { 
                            consultas.Add(new Consulta   //..
                            {
                                Id = reader.GetInt32(0),
                                UtenteId = reader.GetInt32(1),
                                FuncionárioId = reader.GetInt32(2),
                                HospitalId = reader.GetInt32(3),
                                MedicoId = reader.GetInt32(4),
                                Data = reader.GetDateTime(5),
                                Descricao = reader.GetString(7)
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
        public bool DeleteConsulta(int id)
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
        public bool UpdateConsulta(int utenteId, int funcionarioId, int hospitalId, int medicoId, DateTime data,  string descricao)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Consulta SET FuncionarioId = @FuncionarioId, HospitalId = @HospitalId, MedicoId = @MedicoId, Data = @Data,  Descricao = @Descricao WHERE UtenteId = @UtenteId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UtenteId", utenteId);
                        command.Parameters.AddWithValue("@FuncionarioId", funcionarioId);
                        command.Parameters.AddWithValue("@HospitalId", hospitalId);
                        command.Parameters.AddWithValue("@MedicoId", medicoId);
                        command.Parameters.AddWithValue("@Data", data);
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
                                    FuncionárioId = (int)reader["Funcionárioid"],
                                    HospitalId = (int)reader["Hospitalid"],
                                    MedicoId = (int)reader["Medicoid"],
                                    Data = (DateTime)reader["data"],
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
            List<Consulta> consultas = new List<Consulta>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data,  descricao 
                                 FROM Consulta 
                                 WHERE Utenteid = @Utenteid";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Utenteid", utenteId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                consultas.Add(new Consulta
                                {
                                    Id = reader.GetInt32(0),
                                    UtenteId = reader.GetInt32(1),
                                    FuncionárioId = reader.GetInt32(2),
                                    HospitalId = reader.GetInt32(3),
                                    MedicoId = reader.GetInt32(4),
                                    Data = reader.GetDateTime(5),
                                    Descricao = reader.GetString(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar consultas pelo utente", ex);
            }
            return consultas;
        }

        [WebMethod]
        public List<Consulta> ConsultaByFuncionario(int funcionarioId)
        {
            List<Consulta> consultas = new List<Consulta>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data,  descricao 
                                 FROM Consulta 
                                 WHERE Funcionárioid = @Funcionárioid";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Funcionárioid", funcionarioId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                consultas.Add(new Consulta
                                {
                                    Id = reader.GetInt32(0),
                                    UtenteId = reader.GetInt32(1),
                                    FuncionárioId = reader.GetInt32(2),
                                    HospitalId = reader.GetInt32(3),
                                    MedicoId = reader.GetInt32(4),
                                    Data = reader.GetDateTime(5),
                                    Descricao = reader.GetString(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar consultas pelo funcionário", ex);
            }
            return consultas;
        }

        [WebMethod]
        public Consulta ConsultaById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT id, Utenteid, Funcionárioid, Hospitalid, Medicoid, data, descricao 
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
                                    FuncionárioId = reader.GetInt32(2),
                                    HospitalId = reader.GetInt32(3),
                                    MedicoId = reader.GetInt32(4),
                                    Data = reader.GetDateTime(5),
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
    }
}

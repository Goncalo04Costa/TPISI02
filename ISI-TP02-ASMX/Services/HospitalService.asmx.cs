using ISITP02.Models;
using System;
using System.Collections.Generic;
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
                           "f.TipoFuncionárioid, t.Descricao AS TipoFuncionarioDescricao, f.JWT " +
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
                                JWT = reader.GetString(7)
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
            string query = "SELECT Id, Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionárioid, JWT " +
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
                                    JWT = reader.GetString(7)
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
        public bool CreateFuncionario(string nome, int nif, DateTime dataEntrada, int contacto, string password, int tipoFuncionarioId, string jwt)
        {
            string query = "INSERT INTO Funcionário (Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionárioid, JWT) " +
                           "VALUES (@Nome, @NIF, @DataEntrada, @Contacto, @Password, @TipoFuncionárioid, @JWT)";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = nif;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = dataEntrada;
                        command.Parameters.Add("@Contacto", System.Data.SqlDbType.Int).Value = contacto;
                        command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = password;
                        command.Parameters.Add("@TipoFuncionárioid", System.Data.SqlDbType.Int).Value = tipoFuncionarioId;
                        command.Parameters.Add("@JWT", System.Data.SqlDbType.NVarChar, 200).Value = jwt;

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
        public void UpdateFuncionario(int id, string nome, int nif, DateTime dataEntrada, int contacto, string password, int tipoFuncionarioId, string jwt)
        {
            string query = "UPDATE Funcionário SET Nome = @Nome, NIF = @NIF, DataEntrada = @DataEntrada, " +
                           "Contacto = @Contacto, Password = @Password, TipoFuncionárioid = @TipoFuncionárioid, JWT = @JWT WHERE Id = @Id";

            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = nif;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = dataEntrada;
                        command.Parameters.Add("@Contacto", System.Data.SqlDbType.Int).Value = contacto;
                        command.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 100).Value = password;
                        command.Parameters.Add("@TipoFuncionárioid", System.Data.SqlDbType.Int).Value = tipoFuncionarioId;
                        command.Parameters.Add("@JWT", System.Data.SqlDbType.NVarChar, 200).Value = jwt;
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                        command.ExecuteNonQuery();
                    }
                }
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
        public bool CreateUtente(string nome, int nif, DateTime dataEntrada, int tipoUtenteId, int hospitalId)
        {
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
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = nif;
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
        public void UpdateUtente(int id, string nome, int nif, DateTime dataEntrada, int tipoUtenteId, int hospitalId)
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
                        command.Parameters.Add("@Nome", System.Data.SqlDbType.NVarChar, 100).Value = nome;
                        command.Parameters.Add("@NIF", System.Data.SqlDbType.Int).Value = nif;
                        command.Parameters.Add("@DataEntrada", System.Data.SqlDbType.DateTime).Value = dataEntrada;
                        command.Parameters.Add("@TipoUtenteId", System.Data.SqlDbType.Int).Value = tipoUtenteId;
                        command.Parameters.Add("@HospitalId", System.Data.SqlDbType.Int).Value = hospitalId;
                        command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
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
    }
}

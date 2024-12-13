using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class HospitalService : IHospitalService
{

    public HospitalService() { }

    private string connectionString = "";


    public List<Funcionario> GetAllFuncionarios()
    {
        var funcionarios = new List<Funcionario>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "SELECT Id, Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionarioId, JWT FROM Funcionario";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            funcionarios.Add(new Funcionario
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                NIF = (int)reader["NIF"],
                                DataEntrada = (DateTime)reader["DataEntrada"],
                                Contacto = (int)reader["Contacto"],
                                Password = reader["Password"].ToString(),
                                TipoFuncionarioId = (int)reader["TipoFuncionarioId"],
                                JWT = reader["JWT"].ToString()
                            });
                        }
                    }
                }
            }
            return funcionarios;
        }
        catch (Exception ex)
        {
            
            throw new ApplicationException("Error retrieving funcionarios data", ex);
        }
    }

    public Funcionario GetFuncionarioById(int id)
    {
        Funcionario funcionario = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "SELECT Id, Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionarioId, JWT FROM Funcionario WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            funcionario = new Funcionario
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                NIF = (int)reader["NIF"],
                                DataEntrada = (DateTime)reader["DataEntrada"],
                                Contacto = (int)reader["Contacto"],
                                Password = reader["Password"].ToString(),
                                TipoFuncionarioId = (int)reader["TipoFuncionarioId"],
                                JWT = reader["JWT"].ToString()
                            };
                        }
                    }
                }
            }
            return funcionario;
        }
        catch (Exception ex)
        {
            
            throw new ApplicationException("Error retrieving funcionario data", ex);
        }
    }

    public bool CreateFuncionario(string nome, int nif, DateTime dataEntrada, int contacto, string password, int tipoFuncionarioId, string jwt)
    {
        bool isSuccess = false;
        SqlTransaction transaction = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                transaction = connection.BeginTransaction();

                string query = "INSERT INTO Funcionario (Nome, NIF, DataEntrada, Contacto, Password, TipoFuncionarioId, JWT) " +
                               "VALUES (@Nome, @NIF, @DataEntrada, @Contacto, @Password, @TipoFuncionarioId, @JWT)";

                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@NIF", nif);
                    command.Parameters.AddWithValue("@DataEntrada", dataEntrada);
                    command.Parameters.AddWithValue("@Contacto", contacto);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@TipoFuncionarioId", tipoFuncionarioId);
                    command.Parameters.AddWithValue("@JWT", jwt);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                        isSuccess = true;
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            return isSuccess;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            
            throw new ApplicationException("Error creating funcionario", ex);
        }

    }

    public void UpdateFuncionario(int id, string nome, int nif, DateTime dataEntrada, int contacto, string password, int tipoFuncionarioId, string jwt)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "UPDATE Funcionario SET Nome = @Nome, NIF = @NIF, DataEntrada = @DataEntrada, " +
                               "Contacto = @Contacto, Password = @Password, TipoFuncionarioId = @TipoFuncionarioId, JWT = @JWT WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@NIF", nif);
                    command.Parameters.AddWithValue("@DataEntrada", dataEntrada);
                    command.Parameters.AddWithValue("@Contacto", contacto);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@TipoFuncionarioId", tipoFuncionarioId);
                    command.Parameters.AddWithValue("@JWT", jwt);
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
           
            throw new ApplicationException("Error updating funcionario data", ex);
        }
    }

    public bool DeleteFuncionario(int id)
    {
        bool isSuccess = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "DELETE FROM Funcionario WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    isSuccess = rowsAffected > 0;
                }
            }
            return isSuccess;
        }
        catch (Exception ex)
        {
            
            throw new ApplicationException("Error deleting funcionario", ex);
        }
    }

    // Utente Methods
    public List<Utente> GetAllUtentes()
    {
        var utentes = new List<Utente>();

        try
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "SELECT Id, Nome, NIF, DataEntrada, TipoUtenteId, HospitalId FROM Utente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            utentes.Add(new Utente
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                NIF = (int)reader["NIF"],
                                DataEntrada = (DateTime)reader["DataEntrada"],
                                TipoUtenteId = (int)reader["TipoUtenteId"],
                                HospitalId = (int)reader["HospitalId"]
                            });
                        }
                    }
                }
            }
            return utentes;
        }
        catch (Exception ex)
        {

            throw new ApplicationException("Error retrieving utentes data", ex);
        }
    }

    public bool CreateUtente(string nome, int nif, DateTime dataEntrada, int tipoUtenteId, int hospitalId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "INSERT INTO Utente (Nome, NIF, DataEntrada, TipoUtenteId, HospitalId) " +
                               "VALUES (@Nome, @NIF, @DataEntrada, @TipoUtenteId, @HospitalId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@NIF", nif);
                    command.Parameters.AddWithValue("@DataEntrada", dataEntrada);
                    command.Parameters.AddWithValue("@TipoUtenteId", tipoUtenteId);
                    command.Parameters.AddWithValue("@HospitalId", hospitalId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception or handle accordingly
            throw new ApplicationException("Error creating utente", ex);
        }
    }

    public bool DeleteUtente(int id)
    {
        bool isSuccess = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "DELETE FROM Utente WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    isSuccess = rowsAffected > 0;
                }
            }
            return isSuccess;
        }
        catch (Exception ex)
        {
            // Log exception or handle accordingly
            throw new ApplicationException("Error deleting utente", ex);
        }
    }

    // Medico Methods
    public List<Medico> GetAllMedicos()
    {
        var medicos = new List<Medico>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "SELECT Id, Nome, TipoMedicoId FROM Medico";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            medicos.Add(new Medico
                            {
                                Id = (int)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                TipoMedicoId = (int)reader["TipoMedicoId"]
                            });
                        }
                    }
                }
            }
            return medicos;
        }
        catch (Exception ex)
        {
            // Log exception or handle accordingly
            throw new ApplicationException("Error retrieving medicos data", ex);
        }
    }

    public bool CreateMedico(string nome, int tipoMedicoId)
    {
        try
        {
            // teste
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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

    public bool DeleteMedico(int id)
    {
        bool isSuccess = false;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "DELETE FROM Medico WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    isSuccess = rowsAffected > 0;
                }
            }
            return isSuccess;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error deleting medico", ex);
        }
    }
}

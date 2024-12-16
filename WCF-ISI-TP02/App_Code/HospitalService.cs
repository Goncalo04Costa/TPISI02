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
                return funcionarios;
            }
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


    public List<Utente> GetUtentesById()
    {
        var utentes = new List<Utente>();

        try
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "SELECT Id, Nome, NIF, DataEntrada, TipoUtenteId, HospitalId FROM Utente WHERE Id = @Id";

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


    public List<Medico> GetAllMedicos()
    {
        var medicos = new List<Medico>();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString = "Server=tcp:gestaohospitalar.database.windows.net,1433;Initial Catalog=ISItp02;Persist Security Info=False;User ID=TP-ISI;Password=Goncalo18_;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                connection.Open();
                string query = "SELECT Id, Nome, TipoMedicoId FROM Medico WHERE Id = @Id";

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
        
            throw new ApplicationException("Error retrieving medicos data", ex);
        }
    }

    public List<Medico> GetMedicoById()
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



}

using ISITP02.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class HospitalService : IHospitalService
{

    public HospitalService() { }

    private string connectionString = "";


  

  
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

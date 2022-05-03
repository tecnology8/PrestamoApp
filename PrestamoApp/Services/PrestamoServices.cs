using PrestamoApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrestamoApp.Services
{
    public class PrestamoServices
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        public IEnumerable<PrestamoModel> GetPrestamo()
        {
            var lista = new List<PrestamoModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("SP_Prestamos_Get");
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = connection;
                
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var prestamo = new PrestamoModel();

                    prestamo.Id = Convert.ToInt32(reader["Id"]);
                    prestamo.Tipo = reader["Tipo"].ToString();
                    prestamo.Tasa = reader["Tasa"].ToString();

                    lista.Add(prestamo);
                }
                connection.Close();
            }
            return lista;
        }
        public void AddPrestamo(PrestamoModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("SP_Prestamo_Add", connection);
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("@Tasa", model.Tasa);
                
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdatePrestamo(PrestamoModel model)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("SP_Prestamo_Update", connection);
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("@Tasa", model.Tasa);
                
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public PrestamoModel GetById(int? id)
        {
            var prestamo = new PrestamoModel();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Prestamos where Id= " + id;
                var cmd = new SqlCommand(query, connection);
                
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    prestamo.Id = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("Id")));
                    prestamo.Tipo = reader.GetString(reader.GetOrdinal("Tipo"));
                    prestamo.Tasa = reader.GetString(reader.GetOrdinal("Tasa"));
                }
            }
            return prestamo;
        }
        public void DeletePrestamo(int? id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var cmd = new SqlCommand("SP_Prestamo_Delete", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
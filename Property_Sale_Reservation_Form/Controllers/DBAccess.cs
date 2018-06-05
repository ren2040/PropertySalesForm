using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;



namespace Property_Sale_Reservation_Form.Controllers

{
    
    public class DBAccess
    {
        static string connectionString;

        public DBAccess()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ebaseSQLConnection"].ConnectionString;
        }

        public void RecordForm(string addressLine1, string addressLine2, string city,
            string state, string zip, string amount, string firstName, string lastName, string stage2)
        {
            Random rnd = new Random();
            int reference = rnd.Next(10000, 99000);
            List<Int32> referenceData = GetReference();

          
            while (referenceData.Contains(reference))
            {
                reference = rnd.Next(10000, 99000);
            }

            
            string updateSQL = $"INSERT INTO [EBS_DEV_DATA].[dbo].[PROPERTY_SALES](Reference, AddressLine1, AddressLine2, City, StateProvinceRegion, Zip, Amount, FirstName, LastName, Stage2) VALUES('{reference}','{addressLine1}','{addressLine2}','{city}','{state}','{zip}','{amount}','{firstName}','{lastName}','{ stage2}')";

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager
                .ConnectionStrings["ebaseSQLConnection"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(updateSQL,connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
           
        }

        public List<Int32> GetReference()
        {
            List<Int32> referenceData = new List<Int32>();

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager
                .ConnectionStrings["ebaseSQLConnection"].ConnectionString))
            {
                connection.Open();
                string query = "SELECT Reference FROM DBO.PROPERTY_SALES";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            referenceData.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return referenceData;
        }

    }
}
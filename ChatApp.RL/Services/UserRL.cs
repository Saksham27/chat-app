using ChatApp.CL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ChatApp.RL.Services
{
    public class UserRL
    {
        private readonly IConfiguration configuration;

        public UserRL(IConfiguration configure)
        {
            configuration = configure;
        }

        private SqlConnection DatabaseConnection()
        {
            string connectionString = configuration.GetSection("ConnectionString").GetSection("ParkingLotDB").Value;
            return new SqlConnection(connectionString);
        }

        private SqlCommand StoredProcedureConnection(string storedProcedure, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public ShowUserInformation RegisterUser(UserModel data)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {

                //password encrption
                string encryptedPassword = PasswordEncryptDecrypt.EncodePasswordToBase64(data.Password);
                //for store procedure and connection to database
                SqlCommand command = StoredProcedureConnection("spRegisterUser", connection);
                command.Parameters.AddWithValue("@Email", data.EmailID);
                command.Parameters.AddWithValue("@Password", encryptedPassword);
                command.Parameters.AddWithValue("@UserName", data.UserName);
                command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read() == false)
                {
                    return null;
                }
                else
                {
                    return new ShowUserInformation
                    {
                        Id = reader.GetInt32(0),
                        EmailID = reader.GetString(1),
                        UserName = reader.GetString(3),
                        RegistationDate = reader.GetDateTime(4).ToString()
                    };
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();

            }
        }

    }
}

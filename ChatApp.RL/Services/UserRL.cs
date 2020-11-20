using ChatApp.CL.Models;
using ChatApp.RL.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ChatApp.RL.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configuration;

        public UserRL(IConfiguration configure)
        {
            configuration = configure;
        }

        private SqlConnection DatabaseConnection()
        {
            string connectionString = configuration.GetSection("ConnectionString").GetSection("ChatAppDB").Value;
            return new SqlConnection(connectionString);
        }

        private SqlCommand StoredProcedureConnection(string storedProcedure, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(storedProcedure, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public ShowUserInformation UserRegistration(UserModel data)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {

                //password encrption
                string encryptedPassword = PasswordEncryptDecrypt.EncodePasswordToBase64(data.Password);
                //for store procedure and connection to database
                SqlCommand command = StoredProcedureConnection("spRegisterUser", connection);
                command.Parameters.AddWithValue("@EmailID", data.EmailID);
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

        public ShowUserInformation UserLogin(LoginModel data)
        {
            SqlConnection connection = DatabaseConnection();
            try
            {
                SqlCommand command = StoredProcedureConnection("spLoginUser", connection);
                command.Parameters.Add("@EmailID", SqlDbType.VarChar, 50).Value = data.EmailID;
                command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = PasswordEncryptDecrypt.EncodePasswordToBase64(data.Password);
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
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}

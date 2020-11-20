using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.CL.Models
{
    public class PasswordEncryptDecrypt
    {
        /// <summary>
        ///     Encode the Original string into a Encrypted form that ununderstatble for the other user
        /// </summary>
        /// <param name="Password">Store the Password </param>
        /// <returns>Return the Password in the Encrypted Form</returns>
        public static string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}

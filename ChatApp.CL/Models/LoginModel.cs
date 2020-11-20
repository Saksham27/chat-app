using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatApp.CL.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username Is Required")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [RegularExpression("^.{8,15}$", ErrorMessage = "Password Length should be between 8 to 15")]
        public string Password { get; set; }
    }
}

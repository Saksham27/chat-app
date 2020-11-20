using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChatApp.CL.Models
{
    public class ShowUserInformation
    {
        [Required(ErrorMessage = "Id Is Required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "EmailID Is Required")]
        [RegularExpression("^[a-zA-Z0-9]{1,}([.]?[-]?[+]?[a-zA-Z0-9]{1,})?[@]{1}[a-zA-Z0-9]{1,}[.]{1}[a-z]{2,3}([.]?[a-z]{2})?$", ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Username Is Required")]
        public string UserName { get; set; }

        public string RegistationDate { get; set; }
    }
}

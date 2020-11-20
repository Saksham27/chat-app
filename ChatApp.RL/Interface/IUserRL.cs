using ChatApp.CL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.RL.Interface
{
    public interface IUserRL
    {
        ShowUserInformation UserRegistration(UserModel user);
    }
}

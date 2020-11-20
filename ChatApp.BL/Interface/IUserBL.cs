using ChatApp.CL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.BL.Interface
{
    public interface IUserBL
    {
        ResponseMessage<ShowUserInformation> RegisterUser(UserModel data);
        ResponseMessage<ShowUserInformation> LoginUser(LoginModel data);
    }
}

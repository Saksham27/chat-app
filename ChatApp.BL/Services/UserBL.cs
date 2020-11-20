using ChatApp.BL.Interface;
using ChatApp.CL.Models;
using ChatApp.RL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.BL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRepository;

        public UserBL(IUserRL userRepo)
        {
            this.userRepository = userRepo;
        }

        public ResponseMessage<ShowUserInformation> RegisterUser(UserModel data)
        {
            ResponseMessage<ShowUserInformation> response = new ResponseMessage<ShowUserInformation>();
            try
            {

                ShowUserInformation registeredUserDetails = userRepository.UserRegistration(data);
                if (registeredUserDetails != null)
                {
                    response.Status = true;
                    response.Message = "Registration successful";
                    response.Data = registeredUserDetails;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Registration failed. This Email Id or username already exists.";
                    response.Data = registeredUserDetails;
                }
            }
            catch (Exception exception)
            {
                response.Status = false;
                response.Message = "Server error. Error : " + exception.Message;
                response.Data = null;
            }
            return response;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.BL.Interface;
using ChatApp.CL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChatAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration configuration;
        IUserBL userBusinessLayer;
        ResponseMessage<ShowUserInformation> response;

        public UserController(IUserBL userBL, IConfiguration config)
        {
            userBusinessLayer = userBL;
            configuration = config;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult UserRegistration([FromBody] UserModel model)
        {
            try
            {
                response = userBusinessLayer.RegisterUser(model);
                if (response.Status == true)
                {

                    return this.Ok(new { response.Status, response.Message });
                }
                else
                {
                    return BadRequest(new { response.Status, response.Message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

    }
}
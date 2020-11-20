using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatApp.BL.Interface;
using ChatApp.CL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost]
        [Route("login")]
        public ActionResult UserLogin([FromBody] LoginModel model)
        {
            try
            {
                response = userBusinessLayer.LoginUser(model);
                if (response.Status == true)
                {
                    string token = GenerateToken(response.Data);
                    
                    return this.Ok(new { response.Status, response.Message, token});
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

        /// <summary>
        /// Generates Token for Login
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private string GenerateToken(ShowUserInformation Info)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim("Email", Info.EmailID.ToString()),
                    new Claim("UserName", Info.UserName.ToString())
                };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
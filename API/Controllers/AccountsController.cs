using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM) 
        {
            var login = accountRepository.Login(loginVM);
            return login switch
            {
                //1 => Ok(new { status = HttpStatusCode.OK, idToken = login, message = "Login Success"}),
                "2" => BadRequest(new { status = HttpStatusCode.BadRequest, login, message = "Password incorrect" }),
                "3" => BadRequest(new { status = HttpStatusCode.BadRequest, login, message = "Email is not registered" }),
                "4" => BadRequest(new { status = HttpStatusCode.BadRequest, login, message = "The email you entered is empty" }),
                "5" => BadRequest(new { status = HttpStatusCode.BadRequest, login, message = "The password you entered is empty" }),
                _ => Ok(new { status = HttpStatusCode.OK, idToken = login, message = "Login Succes"}),
            };
        }
        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var forgotPassword = accountRepository.ForgotPassword(forgotPasswordVM);
            return forgotPassword switch
            {
                1 => Ok(new { status = HttpStatusCode.OK, forgotPassword, message = "OTP already sent, please check your email" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, forgotPassword, message = "Email not registered" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, forgotPassword, message = "Your email is empty" })
            };
        }
        [HttpPut("ChangePassword")]
        public ActionResult ChengePassword(ChangePasswordVM changePasswordVM)
        {
            var changePassword = accountRepository.ChangePassword(changePasswordVM);
            return changePassword switch
            {
                1 => Ok(new { status = HttpStatusCode.OK, changePassword, message = "Change Password Success" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "OTP expired" }),
                3 => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "OTP already used" }),
                4 => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "OTP incorrect" }),
                5 => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "Email not registered" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, changePassword, message = "Change Password Failed, your email is empty"}),
            };
        }
    }
}

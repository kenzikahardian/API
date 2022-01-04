using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public readonly MyContext myContext;
        public IConfiguration _configuration;

        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }
        public string Login(LoginVM loginVM)
        {
            var result = "0";
            if (loginVM.Email != "" && loginVM.Password != "")
            {
                var cekEmail = myContext.Employees.SingleOrDefault(e => e.Email == loginVM.Email);
                if (cekEmail != null)
                {
                    var cekAccountPass = myContext.Accounts.SingleOrDefault(e => e.NIK == cekEmail.NIK);
                    bool cekPassword = BCrypt.Net.BCrypt.Verify(loginVM.Password, cekAccountPass.Password);
                    if (cekPassword)
                    {
                        var data = (
                        from account in myContext.Accounts
                        join employee in myContext.Employees
                        on account.NIK equals employee.NIK
                        join accountRole in myContext.AccountRoles
                        on account.NIK equals accountRole.NIK
                        join role in myContext.Roles
                        on accountRole.RoleID equals role.RoleID
                        where employee.Email == loginVM.Email
                        select new
                        {
                            email = employee.Email,
                            roles = role.RoleName
                        });
                        var claims = new List<Claim>();
                        claims.Add(new Claim("Email", loginVM.Email));
                        foreach (var item in data)
                        {
                            claims.Add(new Claim("roles", item.roles));
                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                                    _configuration["Jwt:Issuer"],
                                    _configuration["Jwt:Audience"],
                                    claims,
                                    expires: DateTime.UtcNow.AddDays(1),
                                    signingCredentials: signIn
                                    );
                        var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                        claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                        return idtoken;
                    }
                    else
                    {
                        result = "2";
                        return result;
                    }
                }
                else
                {
                    result = "3";
                    return result;
                }
            }
            else if (loginVM.Email == "" && loginVM.Password != "")
            {
                result = "4";
                return result;
            }
            else if (loginVM.Email != "" && loginVM.Password == "")
            {
                result = "5";
                return result;
            }
            else
            {
                result = "0";
                return result;
            }
        }
        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            int result = 0;
            if (forgotPasswordVM.Email !="")
            {
                var checkEmail = myContext.Employees.SingleOrDefault(e => e.Email == forgotPasswordVM.Email);
                if (checkEmail != null)
                {
                    var checkAcc = myContext.Accounts.SingleOrDefault(e => e.NIK == checkEmail.NIK);
                    Random random = new Random();
                    checkAcc.OTP = random.Next(100000, 999999);
                    checkAcc.ExpiredToken = DateTime.Now.AddMinutes(5);

                    var fromAddress = new MailAddress("mcc.61.2021@gmail.com");
                    var passwordFrom = "kahardian61";
                    var toAddress = new MailAddress(forgotPasswordVM.Email);
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, passwordFrom)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = "Forgot Password",
                        Body = "Hai, " + checkEmail.FirstName + " berikut OTP kamu yang sekarang : " + checkAcc.OTP + ". Segera lakukan Change Password",
                        IsBodyHtml = true,
                    }) smtp.Send(message);
                    myContext.Entry(checkAcc).State = EntityState.Modified;
                    myContext.SaveChanges();
                    result = 1;
                }
                else
                {
                    result = 2;
                }
             }
            return result;
        }
        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            int result = 0;
            if (changePasswordVM.Email != "")
            {
                var checkEmail = myContext.Employees.SingleOrDefault(e => e.Email == changePasswordVM.Email);
                if (checkEmail != null)
                {
                    var checkAcc = myContext.Accounts.SingleOrDefault(a => a.NIK == checkEmail.NIK);
                    if (checkAcc.OTP == changePasswordVM.OTP)
                    {
                        if (checkAcc.IsUsed == false)
                        {
                            if (checkAcc.ExpiredToken > DateTime.Now)
                            {
                                checkAcc.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword);
                                checkAcc.IsUsed = true;
                                myContext.Entry(checkAcc).State = EntityState.Modified;
                                myContext.SaveChanges();
                                result = 1;
                            }
                            else
                            {
                                result = 2;
                            }
                        }
                        else
                        {
                            result = 3;
                        }
                    }
                    else
                    {
                        result = 4;
                    }
                }
                else
                {
                    result = 5;
                }
            }
            return result;
        }
    }
}

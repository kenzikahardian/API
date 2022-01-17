using API.Base;
using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;
        public EmployeesController(EmployeeRepository employeeRepository, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.myContext = myContext;
        }
        [HttpPost]
        [Route("/Insert")]
        public ActionResult Inserted(Employee employee)
        {
            var insert = employeeRepository.Inserted(employee);
            return insert switch
            {
                0 => Ok(new { status = HttpStatusCode.OK, message = "Insert Data Successfull" }),
                1 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Insert Failed, Email already exists!" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Insert Failed, Phone already exists!" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Insert Failed, NIK already exists!" }),
            };
        }

        
        [HttpGet]
        //[Authorize(Roles = "Director,Manager")]
        [Route("RegisteredData")]
        public ActionResult RegisteredData()
        {
            var data = employeeRepository.RegisteredData();
            if (data != null)
            {
                
                return Ok(data);
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data is empty" });
            }
        }

        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM) 
        {
            var register = employeeRepository.Register(registerVM);
            if (register == 0)
            {
                return Ok(register);
            }
            else if (register == 1)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Email Duplicate" });
            }
            else if (register == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Phone Duplicate" });
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Can't Insert Data, NIK Duplicate" }); ;
            }
            /*var register = employeeRepository.Register(registerVM);
            if (register >= 1)
            {
                return Ok(new {status = HttpStatusCode.OK, register, message = "Register Success!!" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Register Failed" });*/
        }
        [Authorize(Roles = "Director")]
        [HttpPost]
        [Route("AssignManager")]
        public ActionResult AssignManager(AssignManagerVM assignManagerVM)
        {
            var register = employeeRepository.AssignManager(assignManagerVM);
            return register switch
            {
                1 => Ok(new { status = HttpStatusCode.OK, register, message = "Assign Manager Success" }),
                2 => BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Already assign manager" }),
                _ => BadRequest(new { status = HttpStatusCode.BadRequest, register, message = "Assign Manager Failed" }),
            };
        }
        [HttpPut("{NIK}")]
        public ActionResult Update(string NIK, Employee employee)
        {
            var data = employeeRepository.Get().Count();
            var nik = employeeRepository.Update(NIK, employee);
            if (data != 0)
            {
                return nik switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, message = "Update Success" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Update Failed, Email already exists!" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Update Failed, Phone already exists!" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, message = "NIK not found " }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Update data not successfull, email and phone number already " }),
                };
            }
            else
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Data is empty, can't update data!!" });
            }

        }
        [HttpGet("TestCors")]
        //[EnableCors("AllowOrigin")]
        public ActionResult TestCors()
        {
            return Ok("Test Cors berhasil");
        }
    }
}

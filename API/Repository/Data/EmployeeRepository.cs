using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        public readonly MyContext myContext;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int Inserted(Employee employee)
        {
            int increment = myContext.Employees.ToList().Count;
            var nikIncrement = DateTime.Now.ToString("yyyy") + "0" + increment.ToString();
            var checkNIK = myContext.Employees.Any(x => x.NIK == nikIncrement);
            var checkEmail = myContext.Employees.Any(x => x.Email == employee.Email);
            var checkPhone = myContext.Employees.Any(x => x.Phone == employee.Phone);
            if (checkNIK)
            {
                return 3;
            }
            else
            {
                if (checkEmail)
                {
                    return 1;
                }
                else
                {
                    if (checkPhone)
                    {
                        return 2;
                    }
                    else
                    {
                        employee.NIK = nikIncrement;
                        myContext.Employees.Add(employee);
                        myContext.SaveChanges();
                        return 0;
                    }

                }
            }
        }
        public IEnumerable<Object> RegisteredData()
        {
            var query = from emp in myContext.Employees
                        join acc in myContext.Accounts
                          on emp.NIK equals acc.NIK
                        join prof in myContext.Profilings
                          on acc.NIK equals prof.NIK
                        join edu in myContext.Educations
                          on prof.EducationID equals edu.EducationID
                        join uni in myContext.Universities
                           on edu.UniversityID equals uni.UniversityID
                        select new
                        {   NIK = emp.NIK,
                            FullName = emp.FirstName +" "+ emp.LastName,
                            PhoneNumber = emp.Phone,
                            Gender = emp.Gender.ToString(),
                            Email = emp.Email,
                            BirthDate = emp.BirthDate,
                            Salary = emp.Salary,
                            GPA = edu.GPA,
                            Degree = edu.Degree,
                            UniversityName = uni.UniversityName
                        };
            return query;
        }
        public int Register(RegisterVM registerVM)//bikin pengecekan email dan nomorHp
        {
            var formattedNIK = $"{DateTime.Now.Year + "0" + (myContext.Employees.Count() + 1)}";
            var checkEmail = myContext.Employees.Any(x => x.Email == registerVM.Email);
            var checkPhone = myContext.Employees.Any(x => x.Phone == registerVM.Phone);
            if (checkEmail)
            {
                return 1;
            }
            else if (checkPhone)
            {
                return 2;
            }
            else
            {
                var emp = new Employee
                {
                    NIK = formattedNIK, //bikin variable biar jadi NIK = formatedNIK
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Phone = registerVM.Phone,
                    BirthDate = registerVM.BirthDate,
                    Salary = registerVM.Salary,
                    Email = registerVM.Email,
                    Gender = registerVM.Gender
                };
                myContext.Employees.Add(emp);
                myContext.SaveChanges();
                var acc = new Account
                {
                    NIK = emp.NIK,
                    Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password)
                };
                myContext.Accounts.Add(acc);
                myContext.SaveChanges();
                var accountRole = new AccountRole
                {
                    NIK = emp.NIK,
                    RoleID = 3
                };
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();
                var edu = new Education
                {
                    EducationID = registerVM.EducationID,
                    Degree = registerVM.Degree,
                    GPA = registerVM.GPA,
                    UniversityID = registerVM.UniversityID
                };
                myContext.Educations.Add(edu);
                myContext.SaveChanges();
                var prof = new Profiling
                {
                    NIK = emp.NIK,
                    EducationID = edu.EducationID
                };
                myContext.Profilings.Add(prof);
                myContext.SaveChanges();
                return 0;
            }
        }
        public int AssignManager(AssignManagerVM assignManagerVM)
        {
            int hasil = 1;
            var cekNIK = myContext.Employees.SingleOrDefault(e => e.NIK == assignManagerVM.NIK);
            var cekAccount = myContext.Accounts.SingleOrDefault(a => a.NIK == cekNIK.NIK);
            int temp = 0;
            if (cekNIK != null)
            {
                var cekAccountRole = myContext.AccountRoles.FirstOrDefault(ar => ar.NIK == cekNIK.NIK);
                var cekroleId = myContext.Roles.Where(r => r.AccountRoles.Any(ar => ar.NIK == assignManagerVM.NIK)).ToList();
                foreach (var item in cekroleId)
                {
                    var id = item.RoleID;
                    if (item.RoleID == 2)
                    {
                        temp += 1;
                    }
                }
                if (temp == 1)
                {
                    hasil = 2;
                }
                else
                {
                    var accountrole = new AccountRole
                    {
                        NIK = cekNIK.NIK,
                        RoleID = 2
                    };
                    myContext.AccountRoles.Add(accountrole);
                    myContext.SaveChanges();
                    hasil = 1;
                }
            }
            else
            {
                hasil = 0;
            }
            return hasil;
        }
        public int Update(string NIK, Employee employee)
        {

            var checkData = myContext.Employees.Find(NIK);
            if (checkData != null)
            {
                myContext.Entry(checkData).State = EntityState.Detached;
            }
            else
            {
                return 3;
            }
            if (checkData.Email == employee.Email)
            {
                if (checkData.Phone == employee.Phone)
                {
                    employee.NIK = NIK;
                    myContext.Entry(employee).State = EntityState.Modified;
                    myContext.SaveChanges();
                    return 0;
                }
                else
                {
                    var checkPhone = myContext.Employees.Any(x => x.Phone == employee.Phone);
                    if (checkPhone)
                    {
                        return 2;
                    }
                    else
                    {
                        employee.NIK = NIK;
                        myContext.Entry(employee).State = EntityState.Modified;
                        myContext.SaveChanges();
                        return 0;
                    }
                }
            }
            else
            {
                if (checkData.Phone == employee.Phone)
                {
                    var checkEmail = myContext.Employees.Any(x => x.Email == employee.Email);
                    if (checkEmail)
                    {
                        return 1;
                    }
                    else
                    {
                        employee.NIK = NIK;
                        myContext.Entry(employee).State = EntityState.Modified;
                        myContext.SaveChanges();
                        return 0;
                    }
                }
                else
                {
                    var checkEmailPhone = myContext.Employees.Any(x => x.Email == employee.Email && x.Phone == employee.Phone);
                    if (checkEmailPhone)
                    {
                        return 4;
                    }
                    else
                    {
                        employee.NIK = NIK;
                        myContext.Entry(employee).State = EntityState.Modified;
                        myContext.SaveChanges();
                        return 0;
                    }
                }
            }
        }
      }
}

using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class Old_EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext myContext;
        public Old_EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(string NIK)
        {
            //throw new NotImplementedException();
            var entity = myContext.Employees.Find(NIK);
            if (entity != null)
            {
                myContext.Remove(entity);
                myContext.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }

        }
        //Get All Data
        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            //return myContext.Employees.Find(NIK);
            return myContext.Employees.Where(e => e.NIK == NIK).SingleOrDefault();
            //return myContext.Employees.Where(e => e.NIK == NIK).FirstOrDefault();
        }

        public int Insert(Employee employee) //download postman
        {
            var checkNIK = myContext.Employees.Any(x => x.NIK == employee.NIK);
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
                else if (checkPhone)
                {

                    return 2;
                }
                else
                {
                    myContext.Employees.Add(employee);
                    var result = myContext.SaveChanges();
                    return 0;
                }
            }
            //throw new NotImplementedException();
            /*myContext.Employees.Add(employee);
            var insert = myContext.SaveChanges();
            return insert;*/
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
                /*myContext.Entry(data).State = EntityState.Detached;
                var checkEmail = myContext.Employees.Any(x => x.Email == employee.Email);
                var checkPhone = myContext.Employees.Any(x => x.Phone == employee.Phone);
                //var checkNIK = myContext.Employees.Any(x => x.NIK == NIK);
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
                        employee.NIK = NIK;
                        myContext.Entry(employee).State = EntityState.Modified;
                        myContext.SaveChanges();
                        return 0;
                    }
                }
            }
            else
            {
                return 3;
            }*/
                /*var data = myContext.Employees.Find(NIK);
                if (data != null)
                {
                    myContext.Entry(data).State = EntityState.Detached;
                }
                employee.NIK = NIK;
                myContext.Entry(employee).State = EntityState.Modified;
                var update = myContext.SaveChanges();
                return update;*/
                /*throw new NotImplementedException();
                myContext.Entry(employee).State = EntityState.Modified;
                var update = myContext.SaveChanges();
                return update;*/
            }
        }
    }




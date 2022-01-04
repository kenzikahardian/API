using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get(); //fungsinya untuk get semua data (select*from), IEnumerable : lebih cocok hanya untuk read data, IList untuk manipulasi datanya
        Employee Get(string NIK); //get data sesuai PK => ouput satu data saja | non void
        int Insert(Employee employee);
        int Update(string  NIK, Employee employee);
        int Delete(string NIK);

    }
}

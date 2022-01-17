using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class RegisteredDataVM
    {
        public string NIK { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public float GPA { get; set; }
        public string Degree { get; set; }
        public string UniversityName { get; set; }
    }
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}

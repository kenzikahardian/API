using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_employee")]
    public class Employee
    {
        //use lazy loading without proxies
        /*private Account _Account;
        private ILazyLoader LazyLoader { get; set; }
        public Employee() { }
        private Employee(ILazyLoader lazyLoader) 
        {
            LazyLoader = lazyLoader;
        }
        public Account Account 
        {
            get => LazyLoader.Load(this, ref _Account);
            set => _Account = value;
        }*/

        [Key]
        public string NIK { get; set; }
        [MinLength(2, ErrorMessage = "Firstname minimal 2 karakter")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public int Salary { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.*]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="{0} is a mandatory field")]
        [Range(0, 1, ErrorMessage = "Your Gender is Unavailable")]
        public Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
    }
    public enum Gender 
    {
        Male,
        Female
    }
}

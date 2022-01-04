using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_tr_accountRole")]
    public class AccountRole
    {
        public int AccountRoleID { get; set; }
        public string NIK { get; set; }
        public int RoleID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Role { get; set; }
    }
}

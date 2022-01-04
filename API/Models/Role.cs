﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_role")]
    public class Role 
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}

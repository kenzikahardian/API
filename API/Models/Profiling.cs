﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public int EducationID { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        public virtual Education Education { get; set; }
        
    }
}

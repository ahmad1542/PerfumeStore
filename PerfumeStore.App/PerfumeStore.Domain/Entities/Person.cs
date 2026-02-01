using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PerfumeStore.Domain.Entities
{
    public class Person : BaseEntity
    {
        [Key]
        public string Phone{ get; set; }
        [Required]
        public string Name{ get; set; }
        public Debt? Debt { get; set; }
    }
}

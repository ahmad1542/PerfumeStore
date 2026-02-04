using System;
using System.Collections.Generic;
using System.Text;

namespace PerfumeStore.Domain.Entities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; } = "Ahmad Hussein";
        public string UpdatedBy { get; set; } = "Ahmad Hussein";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

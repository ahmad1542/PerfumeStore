using System;
using System.Collections.Generic;
using System.Text;

namespace PerfumeStore.Domain.Entities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

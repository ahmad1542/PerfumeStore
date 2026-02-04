using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PerfumeStore.Application.Brands.Dtos {
    public class BrandDto {
        public int ID { get; set; }

        public string Name { get; set; } = null!;

        public string? BrandDescription { get; set; }
    }
}

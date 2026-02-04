using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfumeStore.Application.Brands.Commands.CreateBrand {
    public class CreateBrandCommand : IRequest<int> {
        public string Name { get; set; } = default!;
        public string? BrandDescription { get; set; }
    }
}

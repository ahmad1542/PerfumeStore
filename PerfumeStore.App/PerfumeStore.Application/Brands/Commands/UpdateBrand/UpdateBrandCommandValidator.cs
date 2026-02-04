using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerfumeStore.Application.Brands.Commands.UpdateBrand {
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand> {
        public UpdateBrandCommandValidator() {
            RuleFor(dto => dto.Name).Length(3, 150);
        }
    }
}

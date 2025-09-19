using Application.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Validators
{
    public class NewCarValidator : AbstractValidator<NewCar>
    {
        public NewCarValidator()                // This class is validation for "NewCar"
        {
            RuleFor(n => n.Brand)
                .NotEmpty().WithMessage("Car Brand is required.")
                .MaximumLength(15).WithMessage("Brand should not exceed 15 charactors.");
        }
    }
}

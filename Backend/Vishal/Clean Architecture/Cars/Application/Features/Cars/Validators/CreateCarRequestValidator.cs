using Application.Features.Cars.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Validators
{
    public class CreateCarRequestValidator : AbstractValidator<CreateCarRequest>        //As NewCar properties are used by CreateCarRequest 
    {
        public CreateCarRequestValidator()
        {
            RuleFor(request => request.CarRequest)
                .SetValidator(new NewCarValidator());       //invoking one validator in another validator => invoking NewCarValidator inside this class



        }
    }
}

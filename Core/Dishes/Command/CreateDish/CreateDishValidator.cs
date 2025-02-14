using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dishes.Command.CreateDish
{
    public class CreateDishValidator:AbstractValidator<CreateDishCommand>
    {
        public CreateDishValidator()
        {
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
           
            RuleFor(p=>p.KiloCalories).GreaterThanOrEqualTo(0);

            RuleFor(p=>p.Name).NotEmpty().WithMessage("Name Field Cannot Be Empty");
            
            RuleFor(p=>p.Description).NotEmpty().WithMessage("Description Field Cannot Be Empty");
            
        }
    }
}

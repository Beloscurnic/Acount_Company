using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.UpserCompany
{
    public partial class EditCompany
    {
        public class Validation : AbstractValidator<View_Model>
        {
            public Validation()
            {
                RuleFor(x => x.ID)
                    .NotNull();

                RuleFor(x => x.JuridicalName)
                    .NotNull()
                    .WithName("JuridicalName")
                    .MaximumLength(100);

                RuleFor(x => x.CommercialName)
                    .NotNull()
                    .WithName("CommercialName")
                    .MaximumLength(100);

                RuleFor(x => x.IDNO)
                    .NotNull()
                    .WithName("IDNO")
                    .MaximumLength(100);

                //RuleFor(x => x.Email)
                //    .EmailAddress()
                //    .NotNull()
                //    .WithName(Localization.Email)
                //    .MaximumLength(100);

                RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                    .WithName("PhoneNumber")
                    .NotNull()
                    .MinimumLength(8);
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UpserCompany).SetValidator(new Validation());
            }
        }
    }
}

using FluentValidation;

namespace Application.Requests.Commands.UpserCompany
{
    public partial class EditCompany
    {
        public class CompanyUpsertViewModelValidator : AbstractValidator<Command>
        {
            public CompanyUpsertViewModelValidator()
            {
                //RuleFor(x => x.UpserCompany.ID)
                //    .NotNull();

                //RuleFor(x => x.UpserCompany.JuridicalName)
                //    .NotNull()
                //    .WithName("JuridicalName")
                //    .MaximumLength(100);

                //RuleFor(x => x.UpserCompany.CommercialName)
                //    .NotNull()
                //    .WithName("CommercialName")
                //    .MaximumLength(100);

                //RuleFor(x => x.UpserCompany.IDNO)
                //    .NotNull()
                //    .WithName("IDNO")
                //    .MaximumLength(100);

                ////RuleFor(x => x.Email)
                ////    .EmailAddress()
                ////    .NotNull()
                ////    .WithName(Localization.Email)
                ////    .MaximumLength(100);

                //RuleFor(x => x.UpserCompany.PhoneNumber)
                //    .NotEmpty()
                //    .WithName("PhoneNumber")
                //    .NotNull()          
                //    .MinimumLength(8);            

            }
        }
    }
}


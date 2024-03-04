using FluentValidation;

namespace ISAdminWeb.Models.Company
{
    public class Validat : AbstractValidator<UPSertCompany_ViewModel>
    {
        public Validat()
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
            //    .WithName("Email)
            //    .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithName("PhoneNumber")
                .NotNull()
                .MinimumLength(8);

            RuleFor(x => x.JuridicalAddress)
                .NotNull()
                .WithName("JuridicalAddress")
                .MaximumLength(600);

            RuleFor(x => x.OfficeAddress)
                .NotNull()
                .WithName("OfficeAddress")
                .MaximumLength(600);

            RuleFor(x => x.IBAN)
                .NotNull()
                .WithName("IBAN")
                .MaximumLength(100);

            RuleFor(x => x.BIC)
                .NotNull()
                .WithName("BIC")
                .MaximumLength(100);

            RuleFor(x => x.Bank)
                .NotNull()
                .WithName("Bank")
                .MaximumLength(100);

            RuleFor(x => x.VATCode)
                .MaximumLength(100)
                .WithName("VATCode");

            RuleFor(x => x.CountryID)
                .NotNull()
                .WithName("Country");

            RuleFor(x => x.WebSite)
                .MaximumLength(100)
                .WithName("WebSite");

            RuleFor(x => x.ShortName)
                .MaximumLength(11)
                .WithName("ShortName");

        }
    }
}

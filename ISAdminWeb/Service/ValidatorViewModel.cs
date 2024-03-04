using FluentValidation;
using ISAdminWeb.Models.Company;

namespace ISAdminWeb.Service
{
    public class ValidatorViewModel
    {
        public ValidatorViewModel(IServiceCollection services)
        {

           // services.AddTransient<IValidator<UPSertCompany_ViewModel>, Validat>();
        }
    }
}

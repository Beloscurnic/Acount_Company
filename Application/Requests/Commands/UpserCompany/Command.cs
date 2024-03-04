using Application.Global_Models;
using Domain;
using MediatR;

namespace Application.Requests.Commands.UpserCompany
{
    public partial class EditCompany
    {
        public class Command : QueryTokenDelegat, IRequest<CompanyResponse>
        {

            public View_Model UpserCompany { get; set; }
            public Command(View_Model company, string token, Func<string, bool> delegat)
            {
                UpserCompany = company;
                Token = token;
                _Delegat = delegat;
            }
        }
    }

}

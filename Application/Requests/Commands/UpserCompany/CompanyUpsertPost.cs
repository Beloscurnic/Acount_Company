using Domain;

namespace Application.Requests.Commands.UpserCompany
{
    public partial class EditCompany
    {
        public class CompanyUpsertPost
        {
            public Company Company { get; set; }
            public string Token { get; set; }
            public string info { get; set; }
        }
    }
}

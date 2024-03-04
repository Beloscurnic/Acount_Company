using Application.Global_Models;
using Domain;

namespace Application.Requests.Queries.GetCompany.GetCompanyInfo
{
    public partial class GetCompanyInfo
    {
        public class View_Model : ResponseModel
        {
           public  Company Company { get; set; }
        }
    }
}

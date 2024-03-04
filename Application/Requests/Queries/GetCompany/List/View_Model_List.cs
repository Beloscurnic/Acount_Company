using Application.Global_Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Queries.GetCompany.List
{
    public partial class Get_Company_List
    {
        public class View_Model_List: BaseResponse
        {
            public Company[] CompanyList { get; set; }      
        }
    }
}

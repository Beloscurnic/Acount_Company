using Application.Global_Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Queries.GetCompany.CompanyGet
{
    public partial class GetCompany_ID
    {
        public class View_Model : ResponseModel
        {
            public Company Company { get; set; }
        }
    }
}

using Application.Global_Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.UpserCompany
{
    public class CompanyResponse : BaseResponse
    {
        public Company Company { get; set; }
    }
}


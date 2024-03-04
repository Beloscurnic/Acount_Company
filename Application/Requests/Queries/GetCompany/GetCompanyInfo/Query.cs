using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Requests.Queries.GetCompany.List.Get_Company_List;

namespace Application.Requests.Queries.GetCompany.GetCompanyInfo
{
    public partial class GetCompanyInfo
    {
        public class Query : IRequest<View_Model>
        {
            public string Token { get; set; }
            public Func<string, bool> _Delegat { get; set; }
            public int Id { get; set; }
            public Query(string token, Func<string, bool> delegat, int id)
            {
                Token = token;
                _Delegat = delegat;
                Id = id;
            }
        }
    }
}
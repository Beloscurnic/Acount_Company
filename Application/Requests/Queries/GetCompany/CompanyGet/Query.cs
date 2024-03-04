using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Queries.GetCompany.CompanyGet
{
    public partial class GetCompany_ID
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

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Queries.GetCompany.List
{
    public partial class Get_Company_List
    {
        public class Query :IRequest<View_Model_List>
        {
            public string Token { get; set; }
            public Func<string, bool> _Delegat { get; set; }
            public Query(string token, Func<string, bool> delegat)
            {
                Token = token;
                _Delegat = delegat;
            }
        }
    }
}

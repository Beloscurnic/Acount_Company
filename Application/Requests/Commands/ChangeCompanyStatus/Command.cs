using Application.Global_Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.ChangeCompanyStatus
{
    public partial class ChangeCompanyStatus
    {
        public class Command: QueryTokenDelegat, IRequest<BaseResponse>
        {
            public int ID { get; set; }   
            public string Status { get; set; }

            
            public Command(int id , string status, string token, Func<string, bool> delegat )
            {
                ID = id;
                Status = status;
                Token = token;
                _Delegat = delegat;
            }
        }
    }
}

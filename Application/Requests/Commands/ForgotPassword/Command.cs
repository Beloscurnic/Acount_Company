using Application.Global_Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.ForgotPassword
{
    public partial class ForgotPas
    {
        public class Command : IRequest<BaseResponse>
        {
            public string Email { get; set; }
            public string Info { get; set; }

            public Command(string email, string Info)
            {
                Email = email.Trim();
                this.Info = Info;
            }
        }
    }
}

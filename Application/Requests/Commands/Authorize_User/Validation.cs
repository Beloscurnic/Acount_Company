using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.Authorize_User
{
    public partial class Authorize_User
    {
        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {

            }
        }
    }
}

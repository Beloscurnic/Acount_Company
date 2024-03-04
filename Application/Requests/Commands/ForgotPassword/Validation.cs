using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Commands.ForgotPassword
{
    public partial class ForgotPas
    {
        public class Validation : AbstractValidator<Command>
        {
            public Validation()
            {

            }
        }
    }
}

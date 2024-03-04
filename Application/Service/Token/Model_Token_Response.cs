using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Token
{
    public class Model_Token_Response
    {
        public EnErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}

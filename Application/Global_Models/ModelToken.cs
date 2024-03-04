using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Global_Models
{
    public class ModelToken
    {
        public EnErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}

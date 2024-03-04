using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Global_Models
{
    public class QueryTokenDelegat
    {
        public string Token { get; set; }
        public Func<string, bool> _Delegat { get; set; }
    }
}

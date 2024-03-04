using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserClaim
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //public ISCore.Enums.Enums.EnUiLanguage UiLanguage { get; set; }
        public string UiLanguage { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string Password { get; set; }
        public bool IsManaged { get; set; }
    }
}

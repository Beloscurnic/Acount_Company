using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.ISAuthService
{
    public class IAuthURLs
    {
        public string Credentials()
        {
            return "";

        }
        //POST
        public string AuthorizeUser()
        {
            return "/ISAuthService/json/AuthorizeUser";
        }
        //GET
        public string RefreshToken(string Token)
        {
            return "/ISAuthService/json/RefreshToken?Token=" + Token;
        }

        //GET
        public string GetCompanyList(string Token)
        {
            return "/ISAdminWebAppService/json/GetCompanyList?Token=" + Token;
        }

        //GET
        public string GetCompanyInfo(string Token, int ID)
        {
            return "/ISAdminWebAppService/json/GetCompanyInfo?Token=" + Token + "&ID=" + ID;
        }

        //GET
        public string ChangeCompanyStatus(string Token, int ID, int Status)
        {
            return "/ISAdminWebAppService/json/ChangeCompanyStatus?Token=" + Token + "&ID=" + ID + "&Status=" + Status;
        }
        //POST
        public string UPSertCompany()
        {
            return "/ISAdminWebAppService/json/UPSertCompany";
        }
    }
}

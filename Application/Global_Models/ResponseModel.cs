using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Global_Models
{
    public class ResponseModel
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }
        public int OverdueDebtDays { get; set; }
        public int OverdueDebtSum { get; set; }
        public int State { get; set; }
        public int UnpaidDocuments { get; set; }
    }
}

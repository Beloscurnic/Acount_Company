namespace Application.Requests.Commands.UpserCompany
{
    public partial class EditCompany
    {
        public class View_Model
        {
            public int ID { get; set; }
            public string CommercialName { get; set; }
            public string Email { get; set; }
            public string IDNO { get; set; }
            public string JuridicalName { get; set; }
            public string Picture { get; set; }
            public int Status { get; set; }
            public string BIC { get; set; }
            public string Bank { get; set; }
            public int BasePartnerID { get; set; }
            public int CountryID { get; set; }
            public DateTime CreateDate { get; set; }
            public string IBAN { get; set; }
            public bool IsVATPayer { get; set; }
            public string JuridicalAddress { get; set; }
            public string OfficeAddress { get; set; }
            public string PhoneNumber { get; set; }
            public string PostalCode { get; set; }
            public string ShortName { get; set; }
            public string[] ESMAlias { get; set; }
            public string VATCode { get; set; }
            public string WebSite { get; set; }
        }
    }
}


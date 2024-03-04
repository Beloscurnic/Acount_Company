namespace ISAdminWeb.Models
{
    public delegate Task<bool> RefresDelegat(string message);
    public class BaseQueryModel
    {
        public string Token { get; set; }
        public Func<string, bool> _Delegat { get; set; } 
    }
}

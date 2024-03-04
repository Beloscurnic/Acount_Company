using MediatR;

namespace Application.Requests.Commands.Authorize_User
{
    public partial class Authorize_User
    {
        public class Command : IRequest<AuthResponse>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public Command(string email, string password)
            {
                Email = email;
                Password = password;
            }
        }
    }
}

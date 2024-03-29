namespace MarktetingEvent.Database.Authentication.Commands
{
    public class CreateAccountCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> RoleNames { get; set; }
    }
}

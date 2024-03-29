namespace MarktetingEvent.Database.Authentication.Commands
{
    public class ChangePasswordCommand
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}

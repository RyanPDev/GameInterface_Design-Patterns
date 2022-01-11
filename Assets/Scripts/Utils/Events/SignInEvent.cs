public class SignInEvent
{
    public readonly string mail;
    public readonly string password;
    public SignInEvent(string _mail, string _pass)
    {
        mail = _mail;
        password = _pass;
    }
}
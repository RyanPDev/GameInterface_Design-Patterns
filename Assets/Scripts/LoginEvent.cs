public class LoginEvent
{
    public readonly string Text;
    public readonly bool isLogged;

    public LoginEvent(string text)
    {
        Text = text;
    }
}
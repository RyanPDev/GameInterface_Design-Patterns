
public class SignInSuccessfully
{
    public readonly bool signInOk;
    public readonly string exception;
    public SignInSuccessfully(bool b, string _exception = "")
    {
        signInOk = b;
        exception = _exception;
    }

}


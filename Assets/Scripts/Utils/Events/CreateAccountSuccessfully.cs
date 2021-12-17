
public class CreateAccountSuccessfully
{
    public readonly bool createdAccountOk;
    public readonly string exception;
    public CreateAccountSuccessfully(bool b, string _exception = "")
    {
        createdAccountOk = b;
        exception = _exception;
    }
}


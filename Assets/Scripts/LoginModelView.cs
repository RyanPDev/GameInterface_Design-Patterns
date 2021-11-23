using UniRx;

public class LoginModelView
{
    public readonly ReactiveCommand LoginButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;
    //public readonly ReactiveProperty<string> TextID;

    public LoginModelView(/*string _textId*/)
    {
        LoginButtonPressed = new ReactiveCommand();
        IsVisible = new ReactiveProperty<bool>();
        //TextID = new ReactiveProperty<string>(_textId);
    }
}

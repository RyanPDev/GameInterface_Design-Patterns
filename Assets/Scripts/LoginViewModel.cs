using UniRx;

public class LoginViewModel
{
    public readonly ReactiveCommand LoginButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<string> TextID;

    public LoginViewModel()
    {
        LoginButtonPressed = new ReactiveCommand();
        IsVisible = new ReactiveProperty<bool>();
        TextID = new ReactiveProperty<string>(string.Empty);
    }
}
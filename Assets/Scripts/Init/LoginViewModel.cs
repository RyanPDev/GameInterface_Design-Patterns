using UniRx;

public class LoginViewModel : ViewModel
{
    public readonly ReactiveCommand LoginButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<string> TextID;

    public LoginViewModel()
    {
        LoginButtonPressed = new ReactiveCommand().AddTo(_disposables);
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        TextID = new ReactiveProperty<string>(string.Empty).AddTo(_disposables);
    }
}
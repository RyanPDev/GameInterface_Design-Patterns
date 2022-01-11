using UniRx;

public class LoginViewModel : ViewModel
{
    //public readonly ReactiveCommand LoginButtonPressed;
    //public readonly ReactiveProperty<bool> IsAuthenticated;
    public readonly ReactiveProperty<bool> isVisible;

    public LoginViewModel()
    {
        //LoginButtonPressed = new ReactiveCommand().AddTo(_disposables);
        //IsAuthenticated = new ReactiveProperty<bool>().AddTo(_disposables);
        isVisible = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
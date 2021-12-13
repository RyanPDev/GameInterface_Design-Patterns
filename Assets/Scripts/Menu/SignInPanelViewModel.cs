using UniRx;

public class SignInPanelViewModel : ViewModel
    {
    public readonly ReactiveCommand OnBackButtonPressed;
    public readonly ReactiveCommand<SignInEvent> OnSignInButtonPressed;

    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<string> eText;

    public readonly ReactiveProperty<bool> signInAction;

    public SignInPanelViewModel()
    {
        OnBackButtonPressed = new ReactiveCommand()
             .AddTo(_disposables);
        OnSignInButtonPressed = new ReactiveCommand<SignInEvent>()
             .AddTo(_disposables);

        IsVisible = new ReactiveProperty<bool>()
            .AddTo(_disposables);
        signInAction = new ReactiveProperty<bool>()
            .AddTo(_disposables);
        eText = new ReactiveProperty<string>()
           .AddTo(_disposables);
    }
}


using UniRx;
public class SettingsPanelViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveCommand OnSignInButtonPressed;
    public readonly ReactiveCommand OnCreateAccountButtonPressed;

    public SettingsPanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>()
             .AddTo(_disposables); ;
        OnSignInButtonPressed = new ReactiveCommand()
            .AddTo(_disposables);
        OnCreateAccountButtonPressed = new ReactiveCommand()
            .AddTo(_disposables);
    }
}
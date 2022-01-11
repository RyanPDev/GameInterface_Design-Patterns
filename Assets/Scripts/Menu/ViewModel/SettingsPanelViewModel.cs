using UniRx;

public class SettingsPanelViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<bool> IsLoginVisible;
    public readonly ReactiveProperty<bool> IsSignOutVisible;
    public readonly ReactiveProperty<bool> IsCreateVisible;

    public readonly ReactiveCommand OnSignInButtonPressed;
    public readonly ReactiveCommand OnSignOutButtonPressed;
    public readonly ReactiveCommand OnCreateAccountButtonPressed;

    public readonly ReactiveCommand<bool> OnAudioClicked;
    public readonly ReactiveCommand<bool> OnNotificationClicked;

    public SettingsPanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        IsSignOutVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        IsLoginVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        IsCreateVisible = new ReactiveProperty<bool>().AddTo(_disposables);

        OnSignInButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnSignOutButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnCreateAccountButtonPressed = new ReactiveCommand().AddTo(_disposables);

        OnAudioClicked = new ReactiveCommand<bool>().AddTo(_disposables);
        OnNotificationClicked = new ReactiveCommand<bool>().AddTo(_disposables);
    }
}
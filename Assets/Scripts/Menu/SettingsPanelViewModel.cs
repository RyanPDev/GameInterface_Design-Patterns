using UniRx;

public class SettingsPanelViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<bool> IsLoginVisible;
    public readonly ReactiveProperty<bool> IsCreateVisible;

    public readonly ReactiveCommand OnSignInButtonPressed;
    public readonly ReactiveCommand OnCreateAccountButtonPressed;



    public readonly ReactiveProperty<bool> IsAudioOn;
    public readonly ReactiveProperty<bool> IsNotificationsOn;

    public SettingsPanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        IsLoginVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        IsCreateVisible = new ReactiveProperty<bool>().AddTo(_disposables);

        OnSignInButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnCreateAccountButtonPressed = new ReactiveCommand().AddTo(_disposables);

        IsAudioOn = new ReactiveProperty<bool>().AddTo(_disposables);
        IsNotificationsOn = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
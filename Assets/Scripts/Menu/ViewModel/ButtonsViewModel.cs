using UniRx;

public class ButtonsViewModel : ViewModel
{
    public readonly ReactiveCommand OnHomeButtonPressed;
    public readonly ReactiveCommand OnScoreButtonPressed;
    public readonly ReactiveCommand OnSettingsButtonPressed;

    public ButtonsViewModel()
    {
        OnHomeButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnScoreButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnSettingsButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}
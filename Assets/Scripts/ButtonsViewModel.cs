using UniRx;

public class ButtonsViewModel
{
    public readonly ReactiveCommand OnHomeButtonPressed;
    public readonly ReactiveCommand OnScoreButtonPressed;
    public readonly ReactiveCommand OnSettingsButtonPressed;

    public ButtonsViewModel()
    {
        OnHomeButtonPressed = new ReactiveCommand();
        OnScoreButtonPressed = new ReactiveCommand();
        OnSettingsButtonPressed = new ReactiveCommand();
    }
}
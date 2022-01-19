using UniRx;

public class EndGamePanelViewModel : ViewModel
{
    public readonly ReactiveCommand OnContinueButtonPressed;
    public readonly ReactiveCommand OnMenuButtonPressed;

    public readonly ReactiveProperty<bool> IsVisible;

    public readonly ReactiveProperty<bool> gameResult;

    public EndGamePanelViewModel()
    {
        OnContinueButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnMenuButtonPressed = new ReactiveCommand().AddTo(_disposables);

        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        gameResult = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
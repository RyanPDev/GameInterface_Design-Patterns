using UniRx;

public class EndGamePanelViewModel : ViewModel
{
    public readonly ReactiveCommand OnContinueButtonPressed;
    public readonly ReactiveCommand OnMenuButtonPressed;

    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<int> score;
    public readonly ReactiveProperty<int> timer;

    public readonly ReactiveProperty<bool> gameResult;

    public EndGamePanelViewModel()
    {
        OnContinueButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnMenuButtonPressed = new ReactiveCommand().AddTo(_disposables);
        score = new ReactiveProperty<int>(0).AddTo(_disposables);
        timer = new ReactiveProperty<int>(0).AddTo(_disposables);
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        gameResult = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
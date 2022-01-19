using UniRx;

public class GamePanelViewModel : ViewModel
{
    public readonly ReactiveCommand PauseButtonPressed;

    public GamePanelViewModel()
    {
        PauseButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}
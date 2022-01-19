using UniRx;

public class GamePanelViewModel : ViewModel
{
    public readonly ReactiveCommand PauseButtonPressed;

    public readonly ReactiveCollection<LetterViewModel> letter;

    public GamePanelViewModel()
    {
        PauseButtonPressed = new ReactiveCommand().AddTo(_disposables);
        letter = new ReactiveCollection<LetterViewModel>().AddTo(_disposables);
    }
}
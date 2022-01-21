using UniRx;

public class GamePanelViewModel : ViewModel
{
    public readonly ReactiveCommand PauseButtonPressed;

    public readonly ReactiveCollection<LetterViewModel> letter;

    public readonly ReactiveProperty<string> word;

    public readonly ReactiveProperty<bool> newGame;

    public GamePanelViewModel()
    {
        PauseButtonPressed = new ReactiveCommand().AddTo(_disposables);
        letter = new ReactiveCollection<LetterViewModel>().AddTo(_disposables);

        word = new ReactiveProperty<string>(string.Empty).AddTo(_disposables);
        newGame = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
using UniRx;

public class GamePanelViewModel : ViewModel
{
    public readonly ReactiveCommand PauseButtonPressed;
    public readonly ReactiveCommand OnReset;
    public readonly ReactiveCommand OnNewWord;

    public readonly ReactiveProperty<int> wrongNumLetters;
    public readonly ReactiveProperty<int> wordsGuessedCorrectly;
    public readonly ReactiveProperty<int> timer;


    public readonly ReactiveCollection<LetterViewModel> letter;

    public readonly ReactiveProperty<string> word;

    public readonly ReactiveProperty<bool> newGame;

    public GamePanelViewModel()
    {
        PauseButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnReset = new ReactiveCommand().AddTo(_disposables);
        OnNewWord = new ReactiveCommand().AddTo(_disposables);
        letter = new ReactiveCollection<LetterViewModel>().AddTo(_disposables);
        wrongNumLetters = new ReactiveProperty<int>(0).AddTo(_disposables);
        wordsGuessedCorrectly = new ReactiveProperty<int>(0).AddTo(_disposables);
        word = new ReactiveProperty<string>(string.Empty).AddTo(_disposables);
        timer = new ReactiveProperty<int>(0).AddTo(_disposables);
        newGame = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
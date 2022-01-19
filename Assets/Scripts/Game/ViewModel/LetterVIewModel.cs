using UniRx;

public class LetterViewModel : ViewModel
{
    public readonly ReactiveCommand OnLetterButtonPressed;
    public readonly ReactiveProperty<string> letterText;

    public LetterViewModel(string letter)
    {
        OnLetterButtonPressed = new ReactiveCommand().AddTo(_disposables);
        letterText = new ReactiveProperty<string>(letter).AddTo(_disposables);
    }
}
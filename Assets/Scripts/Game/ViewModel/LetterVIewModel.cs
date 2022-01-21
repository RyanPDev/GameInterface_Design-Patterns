using UniRx;
using UnityEngine.UI;
using UnityEngine;

public class LetterViewModel : ViewModel
{
    public readonly ReactiveCommand OnLetterButtonPressed;
    public readonly ReactiveProperty<string> letterText;
    public readonly ReactiveProperty<Color> lettersColor;

    public LetterViewModel(string letter)
    {
        OnLetterButtonPressed = new ReactiveCommand().AddTo(_disposables);
        letterText = new ReactiveProperty<string>(letter).AddTo(_disposables);
        lettersColor = new ReactiveProperty<Color>().AddTo(_disposables);
    }
}
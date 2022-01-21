using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LetterView : View
{
    [SerializeField] public Button letterButton;
    [SerializeField] private TextMeshProUGUI ButtonText;

    [SerializeField] public Image letterColor;

    private LetterViewModel viewModel;

    public void SetViewModel(LetterViewModel _viewModel)
    {
        viewModel = _viewModel;

        letterButton.onClick.AddListener(() =>
        {
            viewModel.OnLetterButtonPressed.Execute();
            letterButton.interactable = false;
        });

        viewModel.letterText.Subscribe((text) =>
        {
            ButtonText.text = text;
        }).AddTo(_disposables);

        viewModel.lettersColor.Subscribe((color) =>
        {
            letterColor.color = color;
        }).AddTo(_disposables);

        letterColor.color = new Color(1, 1, 1);
    }
}
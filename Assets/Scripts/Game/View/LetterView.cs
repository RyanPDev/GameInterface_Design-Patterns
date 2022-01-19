using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LetterView : View
{

    [SerializeField] private Button letterButton;
    [SerializeField] private TextMeshProUGUI ButtonText;

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
        });
    }
}
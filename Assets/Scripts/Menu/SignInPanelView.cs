using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

class SignInPanelView : View
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button signInButton;
    [SerializeField] private TextMeshProUGUI ButtonText;
    [SerializeField] private TextMeshProUGUI errorText;

    [SerializeField] private TMP_InputField inputFieldMail;
    [SerializeField] private TMP_InputField inputFieldPassword;
    private SignInPanelViewModel viewModel;

    public void SetViewModel(SignInPanelViewModel _viewModel)
    {
        viewModel = _viewModel;
        errorText.text = "";
        errorText.color = Color.red;
        viewModel
            .IsVisible
            .Subscribe((isVisible) =>
            {
                gameObject.SetActive(isVisible);
            })
            .AddTo(_disposables);

        viewModel
           .signInAction
           .Subscribe((_signInAction) =>
           {
               if (_signInAction)
                   ButtonText.SetText("SIGN IN");
               else
                   ButtonText.SetText("REGISTER");
           })
           .AddTo(_disposables);

        viewModel
         .eText
         .Subscribe((_eText) =>
         {
             errorText.text = _eText;
         })
         .AddTo(_disposables);

        backButton.onClick.AddListener(() =>
        {
            _viewModel.OnBackButtonPressed.Execute();
        });

        signInButton.onClick.AddListener(() =>
        {
            _viewModel.OnSignInButtonPressed.Execute(new SignInEvent(inputFieldMail.text, inputFieldPassword.text));
        });
    }
}
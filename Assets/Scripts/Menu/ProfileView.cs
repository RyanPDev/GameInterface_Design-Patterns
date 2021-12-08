using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


class ProfileView : View
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private TMP_InputField inputField;
    private ProfileViewModel viewModel;

    public void SetViewModel(ProfileViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel
            .IsVisible
            .Subscribe((isVisible) => {
                gameObject.SetActive(isVisible);
            })
            .AddTo(_disposables);
        _viewModel
            .UserName
            .Subscribe(taskName =>
            {
                inputField.SetTextWithoutNotify(taskName);
            })
            .AddTo(_disposables);

        backButton.onClick.AddListener(() =>
        {
            _viewModel.OnBackButtonPressed.Execute();
        });

        saveButton.onClick.AddListener(() =>
        {
            _viewModel.OnSaveButtonPressed.Execute(inputField.text);
        });
    }

}


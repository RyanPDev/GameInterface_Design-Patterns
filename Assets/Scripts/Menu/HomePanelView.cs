using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class HomePanelView : MonoBehaviour
{
    private HomePanelViewModel viewModel;

    [SerializeField] private Button profileButton;
    [SerializeField] private TextMeshProUGUI userNameText;

    public void SetViewModel(HomePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        });
        viewModel.Username.Subscribe((username) =>
        {
            userNameText.text = username;
        });

        profileButton.onClick.AddListener(() =>
        {
            viewModel.ProfileButtonPressed.Execute();
        });        
    }
}
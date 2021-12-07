using UnityEngine;
using UniRx;
using DG.Tweening;

public class HomePanelView : MonoBehaviour
{
    private HomePanelViewModel _viewModel;

    public void SetViewModel(HomePanelViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        });
    }
}
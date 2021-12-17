using UnityEngine;
using UniRx;
using DG.Tweening;

public class ScorePanelView : View
{
    private ScorePanelViewModel viewModel;

    public void SetViewModel(ScorePanelViewModel _viewModel)
    {
        viewModel = _viewModel;

        viewModel.IsVisible.Subscribe((isVisible) =>
        {
            gameObject.SetActive(isVisible);
            gameObject.GetComponent<RectTransform>().DOLocalMoveX(5, 0f);
            gameObject.GetComponent<RectTransform>().DOMoveX(0, .2f);
        })
        .AddTo(_disposables);
    }
}
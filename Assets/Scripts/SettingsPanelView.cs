using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class SettingsPanelView : MonoBehaviour
{
    private SettingsPanelViewModel _viewModel;

    public void SetViewModel(SettingsPanelViewModel viewModel)
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
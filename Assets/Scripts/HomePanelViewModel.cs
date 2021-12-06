using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HomePanelViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;

    public HomePanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
    }
}
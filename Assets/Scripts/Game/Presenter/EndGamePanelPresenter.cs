using UnityEngine;
using UniRx;
using System;

class EndGamePanelPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly EndGamePanelViewModel endGamePanelViewModel;

    public EndGamePanelPresenter(EndGamePanelViewModel _endGamePanelViewModel)
    {
        endGamePanelViewModel = _endGamePanelViewModel;
    }


    public override void Dispose()
    {
        base.Dispose();
    }
}

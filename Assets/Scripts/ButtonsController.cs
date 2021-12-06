using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ButtonsController
{
    public ButtonsController(HomePanelViewModel homePanelViewModel, ScorePanelViewModel scorePanelViewModel,
        SettingsPanelViewModel settingsPanelViewModel, ButtonsViewModel buttonsViewModel)
    {
        homePanelViewModel.IsVisible.Value = true;

        buttonsViewModel.OnHomeButtonPressed.Subscribe((_) =>
        {
            homePanelViewModel.IsVisible.Value = true;
            scorePanelViewModel.IsVisible.Value = false;
            settingsPanelViewModel.IsVisible.Value = false;
        });

        buttonsViewModel.OnScoreButtonPressed.Subscribe((_) =>
        {
            homePanelViewModel.IsVisible.Value = false;
            scorePanelViewModel.IsVisible.Value = true;
            settingsPanelViewModel.IsVisible.Value = false;
        });

        buttonsViewModel.OnSettingsButtonPressed.Subscribe((_) =>
        {
            homePanelViewModel.IsVisible.Value = false;
            scorePanelViewModel.IsVisible.Value = false;
            settingsPanelViewModel.IsVisible.Value = true;
        });
    }
}
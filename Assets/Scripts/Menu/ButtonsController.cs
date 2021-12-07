using UniRx;

public class ButtonsController
{
    private readonly HomePanelViewModel homePanelViewModel;
    private readonly ScorePanelViewModel scorePanelViewModel;
    private readonly SettingsPanelViewModel settingsPanelViewModel;
    private readonly ButtonsViewModel buttonsViewModel;

    public ButtonsController(HomePanelViewModel _homePanelViewModel, ScorePanelViewModel _scorePanelViewModel,
        SettingsPanelViewModel _settingsPanelViewModel, ButtonsViewModel _buttonsViewModel)
    {
        homePanelViewModel = _homePanelViewModel;
        scorePanelViewModel = _scorePanelViewModel;
        settingsPanelViewModel = _settingsPanelViewModel;
        buttonsViewModel = _buttonsViewModel;

        homePanelViewModel.IsVisible.Value = true;

        buttonsViewModel.OnHomeButtonPressed.Subscribe((_) =>
        {
            SetPanelVisibility(true, false, false);
        });

        buttonsViewModel.OnScoreButtonPressed.Subscribe((_) =>
        {
            SetPanelVisibility(false, true, false);
        });

        buttonsViewModel.OnSettingsButtonPressed.Subscribe((_) =>
        {
            SetPanelVisibility(false, false, true);
        });
    }

    private void SetPanelVisibility(bool homeVisibility, bool scoreVisibility, bool settingsVisibility)
    {
        homePanelViewModel.IsVisible.Value = homeVisibility;
        scorePanelViewModel.IsVisible.Value = scoreVisibility;
        settingsPanelViewModel.IsVisible.Value = settingsVisibility;
    }
}
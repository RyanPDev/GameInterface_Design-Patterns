using UnityEngine;

public class MenuInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private HomePanelView _homePanelPrefab;
    [SerializeField] private ScorePanelView _scorePanelPrefab;
    [SerializeField] private SettingsPanelView _settingsPanelPrefab;
    [SerializeField] private ButtonsView _buttonsPrefab;

    private void Awake()
    {

        var _homePanelView = Instantiate(_homePanelPrefab, canvasParent);
        var _scorePanelView = Instantiate(_scorePanelPrefab, canvasParent);
        var _settingsPanelView = Instantiate(_settingsPanelPrefab, canvasParent);
        var _buttonsView = Instantiate(_buttonsPrefab, canvasParent);

        var homePanelViewModel = new HomePanelViewModel();
        var scorePanelViewModel = new ScorePanelViewModel();
        var settingsPanelViewModel = new SettingsPanelViewModel();
        var buttonsViewModel = new ButtonsViewModel();

        _homePanelView.SetViewModel(homePanelViewModel);
        _scorePanelView.SetViewModel(scorePanelViewModel);
        _settingsPanelView.SetViewModel(settingsPanelViewModel);
        _buttonsView.SetViewModel(buttonsViewModel);

        new ButtonsController(homePanelViewModel, scorePanelViewModel, settingsPanelViewModel, buttonsViewModel);
    }




}
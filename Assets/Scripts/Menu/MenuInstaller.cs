using UnityEngine;

public class MenuInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private HomePanelView _homePanelPrefab;
    [SerializeField] private ProfileView _profileViewPrefab;
    [SerializeField] private ScorePanelView _scorePanelPrefab;
    [SerializeField] private SettingsPanelView _settingsPanelPrefab;
    [SerializeField] private ButtonsView _buttonsPrefab;

    private void Awake()
    {
        var userRepository = ServiceLocator.Instance.GetService<IUserDataAccess>();   
        var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();   

        var _homePanelView = Instantiate(_homePanelPrefab, canvasParent);
        var _profilePanelView = Instantiate(_profileViewPrefab, canvasParent); //----->profile
        var _scorePanelView = Instantiate(_scorePanelPrefab, canvasParent);
        var _settingsPanelView = Instantiate(_settingsPanelPrefab, canvasParent);
        var _buttonsView = Instantiate(_buttonsPrefab, canvasParent);

        var homePanelViewModel = new HomePanelViewModel();        
        var profilePanelViewModel = new ProfileViewModel(); //----->profile
        var scorePanelViewModel = new ScorePanelViewModel();
        var settingsPanelViewModel = new SettingsPanelViewModel();
        var buttonsViewModel = new ButtonsViewModel();
    
        _homePanelView.SetViewModel(homePanelViewModel);
        _profilePanelView.SetViewModel(profilePanelViewModel); //----->profile
        _scorePanelView.SetViewModel(scorePanelViewModel);
        _settingsPanelView.SetViewModel(settingsPanelViewModel);
        _buttonsView.SetViewModel(buttonsViewModel);

        var updateUserUseCase = new UpdateUsernameUseCase(userRepository, eventDispatcher);

        new ButtonsController(homePanelViewModel, scorePanelViewModel, settingsPanelViewModel, buttonsViewModel);

        new HomePanelController(homePanelViewModel, profilePanelViewModel);
        new ProfileController(profilePanelViewModel, updateUserUseCase);
        new HomePanelPresenter(homePanelViewModel, eventDispatcher);
    }

    //private static ITaskRepository GetTaskRepository()
    //{
    //    return new LocalTaskRepository();
    //}
}
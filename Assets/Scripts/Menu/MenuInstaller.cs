using UnityEngine;

public class MenuInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private HomePanelView _homePanelPrefab;
    [SerializeField] private ProfileView _profileViewPrefab;
    [SerializeField] private ScorePanelView _scorePanelPrefab;
    [SerializeField] private SignInPanelView _signInPanelPrefab;
    [SerializeField] private SettingsPanelView _settingsPanelPrefab;
    [SerializeField] private ButtonsView _buttonsPrefab;

    private void Awake()
    {
        var userRepository = ServiceLocator.Instance.GetService<IUserDataAccess>();
        var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();

        var _settingsPanelView = Instantiate(_settingsPanelPrefab, canvasParent);
        var _signInPanelView = Instantiate(_signInPanelPrefab, canvasParent);
        var _homePanelView = Instantiate(_homePanelPrefab, canvasParent);
        var _profilePanelView = Instantiate(_profileViewPrefab, canvasParent);
        var _scorePanelView = Instantiate(_scorePanelPrefab, canvasParent);
        var _buttonsView = Instantiate(_buttonsPrefab, canvasParent);


        var signInPanelViewModel = new SignInPanelViewModel();
        var settingsPanelViewModel = new SettingsPanelViewModel();
        var homePanelViewModel = new HomePanelViewModel();
        var profilePanelViewModel = new ProfileViewModel();
        var scorePanelViewModel = new ScorePanelViewModel();
        var buttonsViewModel = new ButtonsViewModel();

        _settingsPanelView.SetViewModel(settingsPanelViewModel);
        _signInPanelView.SetViewModel(signInPanelViewModel);
        _homePanelView.SetViewModel(homePanelViewModel);
        _profilePanelView.SetViewModel(profilePanelViewModel);
        _scorePanelView.SetViewModel(scorePanelViewModel);
        _buttonsView.SetViewModel(buttonsViewModel);

        var updateUserUseCase = new UpdateUsernameUseCase(userRepository, eventDispatcher);
        var accountManager = new AccountManagerUseCase(eventDispatcher);

        new ButtonsController(homePanelViewModel, scorePanelViewModel, settingsPanelViewModel, buttonsViewModel);

        new SettingsPanelController(settingsPanelViewModel, signInPanelViewModel);
        new SettingsPanelPresenter(settingsPanelViewModel, eventDispatcher);
        new SignInController(signInPanelViewModel, accountManager);
        new SignInPresenter(signInPanelViewModel, eventDispatcher);
        new HomePanelController(homePanelViewModel, profilePanelViewModel);
        new ProfileController(profilePanelViewModel, updateUserUseCase);
        new HomePanelPresenter(homePanelViewModel, eventDispatcher, userRepository.GetLocalUser());
    }
}
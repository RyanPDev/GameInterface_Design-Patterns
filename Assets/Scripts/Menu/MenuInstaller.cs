using System;
using System.Collections.Generic;
using UniRx;
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

    private List<IDisposable> _disposables = new List<IDisposable>();
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }

    private void Awake()
    {
        var userRepository = ServiceLocator.Instance.GetService<IUserDataAccess>();
        var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();
        var firebaseAccountService = ServiceLocator.Instance.GetService<IFirebaseAccountService>();
        var firebaseLoginService = ServiceLocator.Instance.GetService<IFirebaseLoginService>();

        var _settingsPanelView = Instantiate(_settingsPanelPrefab, canvasParent);
        var _signInPanelView = Instantiate(_signInPanelPrefab, canvasParent);
        var _homePanelView = Instantiate(_homePanelPrefab, canvasParent);
        var _profilePanelView = Instantiate(_profileViewPrefab, canvasParent);
        var _scorePanelView = Instantiate(_scorePanelPrefab, canvasParent);
        var _buttonsView = Instantiate(_buttonsPrefab, canvasParent);

        var signInPanelViewModel = new SignInPanelViewModel().AddTo(_disposables);
        var settingsPanelViewModel = new SettingsPanelViewModel().AddTo(_disposables);
        var homePanelViewModel = new HomePanelViewModel().AddTo(_disposables);
        var profilePanelViewModel = new ProfileViewModel().AddTo(_disposables);
        var scorePanelViewModel = new ScorePanelViewModel().AddTo(_disposables);
        var buttonsViewModel = new ButtonsViewModel().AddTo(_disposables);

        _settingsPanelView.SetViewModel(settingsPanelViewModel, userRepository);
        _signInPanelView.SetViewModel(signInPanelViewModel);
        _homePanelView.SetViewModel(homePanelViewModel);
        _profilePanelView.SetViewModel(profilePanelViewModel);
        _scorePanelView.SetViewModel(scorePanelViewModel);
        _buttonsView.SetViewModel(buttonsViewModel);

        var updateUserUseCase = new UpdateUserUseCase(firebaseLoginService, userRepository, eventDispatcher).AddTo(_disposables);
        var changeSceneUseCase = new ChangeSceneUseCase(eventDispatcher);
        var accountManager = new AccountManagerUseCase(firebaseAccountService, eventDispatcher).AddTo(_disposables);

        new ButtonsController(homePanelViewModel, scorePanelViewModel, settingsPanelViewModel, buttonsViewModel).AddTo(_disposables);

        new FirebasePushUpService(eventDispatcher);
        new SettingsPanelController(settingsPanelViewModel, signInPanelViewModel, updateUserUseCase, accountManager).AddTo(_disposables);
        new SettingsPanelPresenter(settingsPanelViewModel, eventDispatcher).AddTo(_disposables);
        new SignInController(signInPanelViewModel, accountManager).AddTo(_disposables);
        new SignInPresenter(signInPanelViewModel, eventDispatcher).AddTo(_disposables);
        new HomePanelController(homePanelViewModel, profilePanelViewModel, changeSceneUseCase).AddTo(_disposables);
        new ProfileController(profilePanelViewModel, updateUserUseCase).AddTo(_disposables);
        new HomePanelPresenter(homePanelViewModel, eventDispatcher, userRepository.GetLocalUser()).AddTo(_disposables);
    }
}
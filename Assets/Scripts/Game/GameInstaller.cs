using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform canvasParent;
    [SerializeField] private GamePanelView _gamePanelPrefab;
    [SerializeField] private PausePanelView _pausePanelPrefab;

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

        var gamePanelView = Instantiate(_gamePanelPrefab, canvasParent);
        var pausePanelView = Instantiate(_pausePanelPrefab, canvasParent);

        var gamePanelViewModel = new GamePanelViewModel().AddTo(_disposables);
        var pausePanelViewModel = new PausePanelViewModel().AddTo(_disposables);

        gamePanelView.SetViewModel(gamePanelViewModel);
        pausePanelView.SetViewModel(pausePanelViewModel);

        var changeSceneUseCase = new ChangeSceneUseCase(eventDispatcher);

        //new GamePanelPresenter(gamePanelViewModel, eventDispatcher).AddTo(_disposables);
        new GamePanelController(gamePanelViewModel, pausePanelViewModel).AddTo(_disposables);
        new PausePanelController(pausePanelViewModel, changeSceneUseCase).AddTo(_disposables);
    }
}
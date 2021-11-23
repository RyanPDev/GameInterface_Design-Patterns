//using Code;
using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;
    [SerializeField] private LoginView _loginPanelPrefab;
    
    private void Awake()
    {
        var loginPanelView = Instantiate(_loginPanelPrefab, _canvasParent);
        var loginPanelViewModel = new LoginModelView();

        loginPanelView.SetViewModel(loginPanelViewModel);
        new LoginController(loginPanelViewModel);
        // aqui va el presenter
        
        //taskRespoitory
        //var eventDispatcher = new EventDispatcherService();
    }
}

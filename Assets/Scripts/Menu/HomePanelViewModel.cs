using UniRx;

public class HomePanelViewModel : ViewModel
{
    public readonly ReactiveCommand ProfileButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<string> Username;

    public HomePanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
        Username = new ReactiveProperty<string>(string.Empty);
        ProfileButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}
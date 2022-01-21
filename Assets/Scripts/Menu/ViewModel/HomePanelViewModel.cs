using UniRx;

public class HomePanelViewModel : ViewModel
{
    public readonly ReactiveCommand ProfileButtonPressed;
    public readonly ReactiveCommand PlayButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<string> Username;

    public HomePanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
        Username = new ReactiveProperty<string>(string.Empty).AddTo(_disposables);
        ProfileButtonPressed = new ReactiveCommand().AddTo(_disposables);
        PlayButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}
using UniRx;

public class HomePanelViewModel : ViewModel
{
    public readonly ReactiveCommand ProfileButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;

    public HomePanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
        ProfileButtonPressed = new ReactiveCommand().AddTo(_disposables);
    }
}
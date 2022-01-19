using UniRx;

class PausePanelViewModel : ViewModel
{
    public readonly ReactiveCommand OnResumeButtonPressed;
    public readonly ReactiveCommand OnMenuButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;

    public PausePanelViewModel()
    {
        OnResumeButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnMenuButtonPressed = new ReactiveCommand().AddTo(_disposables);
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
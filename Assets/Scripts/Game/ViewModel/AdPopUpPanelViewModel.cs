using UniRx;

class AdPopUpPanelViewModel : ViewModel
{
    public readonly ReactiveCommand OnAdButtonPressed;
    public readonly ReactiveCommand OnMenuButtonPressed;
    public readonly ReactiveProperty<bool> IsVisible;

    public AdPopUpPanelViewModel()
    {
        OnAdButtonPressed = new ReactiveCommand().AddTo(_disposables);
        OnMenuButtonPressed = new ReactiveCommand().AddTo(_disposables);
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
using UniRx;

public class ScorePanelViewModel : ViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;

    public ScorePanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>().AddTo(_disposables);
    }
}
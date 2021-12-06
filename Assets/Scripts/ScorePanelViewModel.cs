using UniRx;

public class ScorePanelViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;

    public ScorePanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
    }
}
using UniRx;
public class SettingsPanelViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;

    public SettingsPanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
    }
}
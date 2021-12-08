using UniRx;

class ProfileViewModel : ViewModel
    {
     public readonly ReactiveCommand OnBackButtonPressed;
     public readonly ReactiveCommand<string> OnSaveButtonPressed;

     public readonly ReactiveProperty<bool> IsVisible;
     public readonly ReactiveProperty<string> UserName;

     public ProfileViewModel()
     {
        OnBackButtonPressed = new ReactiveCommand()
             .AddTo(_disposables);
        OnSaveButtonPressed = new ReactiveCommand<string>()
             .AddTo(_disposables);

         IsVisible = new ReactiveProperty<bool>()
             .AddTo(_disposables);
        UserName = new ReactiveProperty<string>(string.Empty)
             .AddTo(_disposables);
     }
}


using UniRx;

class GamePanelPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly GamePanelViewModel viewModel;
    private readonly IUpdateGameUseCase updateGameUseCase;

    public GamePanelPresenter(GamePanelViewModel gamePanelViewModel, IUpdateGameUseCase _updateGameUseCase, IEventDispatcherService _eventDispatcher)
    {
        viewModel = gamePanelViewModel;
        eventDispatcherService = _eventDispatcher;
        updateGameUseCase = _updateGameUseCase;

        eventDispatcherService.Subscribe<GetLetterEvent>(GetLetter);
    }

    private void GetLetter(GetLetterEvent letter)
    {
        var letterViewModel = new LetterViewModel(letter.v.ToString());

        letterViewModel.OnLetterButtonPressed.Subscribe((_) =>
        {
            updateGameUseCase.CheckLetter(letterViewModel.letterText.Value);
        });

        viewModel.letter.Add(letterViewModel);
    }
}

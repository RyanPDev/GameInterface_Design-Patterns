using UnityEngine;
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

        eventDispatcherService.Subscribe<GetLetterEvent>(GetLetters);
        eventDispatcherService.Subscribe<GetWordEvent>(GetWord);
        eventDispatcherService.Subscribe<CheckLetterEvent>(UpdateLetterColor);
    }

    private void UpdateLetterColor(CheckLetterEvent obj)
    {
        foreach (LetterViewModel element in viewModel.letter)
        {
            if (element.letterText.Value == obj.l)
            {
                if (obj.v)
                {
                    element.letterColor.Value = new Color(0, 1, 0);
                }
                else
                {
                    element.letterColor.Value = new Color(1, 0, 0);
                }
                break;
            }            
        }
    }

    private void GetWord(GetWordEvent obj)
    {
        viewModel.word.SetValueAndForceNotify(obj.v);
    }

    private void GetLetters(GetLetterEvent letter)
    {
        var letterViewModel = new LetterViewModel(letter.v.ToString());

        letterViewModel.OnLetterButtonPressed.Subscribe((_) =>
        {
            updateGameUseCase.CheckLetter(letterViewModel.letterText.Value);
        });

        viewModel.letter.Add(letterViewModel);
    }
}

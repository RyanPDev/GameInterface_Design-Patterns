using UnityEngine;
using UniRx;
using System;

class GamePanelPresenter : Presenter
{
    private readonly IEventDispatcherService eventDispatcherService;
    private readonly GamePanelViewModel gamePanelViewModel;
    private readonly EndGamePanelViewModel endGamePanelViewModel;
    private readonly IUpdateGameUseCase updateGameUseCase;

    public GamePanelPresenter(GamePanelViewModel _gamePanelViewModel, EndGamePanelViewModel _endGamePanelViewModel, IUpdateGameUseCase _updateGameUseCase, IEventDispatcherService _eventDispatcher)
    {
        gamePanelViewModel = _gamePanelViewModel;
        endGamePanelViewModel = _endGamePanelViewModel;
        eventDispatcherService = _eventDispatcher;
        updateGameUseCase = _updateGameUseCase;

        eventDispatcherService.Subscribe<GetLetterEvent>(GetLetters);
        eventDispatcherService.Subscribe<GetWordEvent>(GetWord);
        eventDispatcherService.Subscribe<CheckLetterEvent>(UpdateLetterColor);
        eventDispatcherService.Subscribe<EndEvent>(EndPanelPopUp);
    }

    private void EndPanelPopUp(EndEvent obj)
    {
        endGamePanelViewModel.gameResult.Value = obj.v;
        endGamePanelViewModel.IsVisible.Value = true;
    }

    private void UpdateLetterColor(CheckLetterEvent obj)
    {
        foreach (LetterViewModel element in gamePanelViewModel.letter)
        {
            if (element.letterText.Value == obj.l)
            {
                if (obj.v)
                {
                    element.letterColor.Value = new Color(0.18f, 0.5f, 0.18f);
                }
                else
                {
                    element.letterColor.Value = new Color(0.7f, 0, 0);
                }
                break;
            }            
        }
    }

    private void GetWord(GetWordEvent obj)
    {
        gamePanelViewModel.word.SetValueAndForceNotify(obj.v);
    }

    private void GetLetters(GetLetterEvent letter)
    {
        var letterViewModel = new LetterViewModel(letter.v.ToString());

        letterViewModel.OnLetterButtonPressed.Subscribe((_) =>
        {
            updateGameUseCase.CheckLetter(letterViewModel.letterText.Value);
        });

        gamePanelViewModel.letter.Add(letterViewModel);
    }
}

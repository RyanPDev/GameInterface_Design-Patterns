using UnityEngine;
using UniRx;

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
        gamePanelViewModel.OnReset.Execute();
        if (obj.v)
        {
            gamePanelViewModel.letter.Clear();
            gamePanelViewModel.OnNewWord.Execute();
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                GetLetters(new GetLetterEvent(letters[i]));
            }
        }
        
    }

    private void UpdateLetterColor(CheckLetterEvent obj)
    {
        foreach (LetterViewModel element in gamePanelViewModel.letter)
        {
            if (element.letterText.Value == obj.l)
            {
                if (obj.v)
                {
                    element.lettersColor.Value = new Color(0.18f, 0.5f, 0.18f);
                }
                else
                {
                    element.lettersColor.Value = new Color(0.7f, 0, 0);
                    gamePanelViewModel.wrongNumLetters.Value++;
                    if (gamePanelViewModel.wrongNumLetters.Value >= 9)
                    {

                        // LOSE
                        eventDispatcherService.Dispatch<EndEvent>(new EndEvent(false));
                    }
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
        }).AddTo(_disposables);

        gamePanelViewModel.letter.Add(letterViewModel);
    }

    public override void Dispose()
    {
        base.Dispose();

        eventDispatcherService.Unsubscribe<GetLetterEvent>(GetLetters);
        eventDispatcherService.Unsubscribe<GetWordEvent>(GetWord);
        eventDispatcherService.Unsubscribe<CheckLetterEvent>(UpdateLetterColor);
        eventDispatcherService.Unsubscribe<EndEvent>(EndPanelPopUp);
    }
}

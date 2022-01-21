using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangmanService : Service, IHangmanService
{
    private string _token;
    private StringBuilder _correctLetters;
    private StringBuilder _incorrectLetters;
    private HangmanClient _restClientAdapter;

    readonly IEventDispatcherService eventDispatcher;

    public HangmanService(IEventDispatcherService _eventDispatcher)
    {
        eventDispatcher = _eventDispatcher;

        _restClientAdapter = new HangmanClient();
        _correctLetters = new StringBuilder();
        _incorrectLetters = new StringBuilder();
    }
    public async Task InitAsync()
    {
        GetLetters();
        await StartGame();
    }

    public void GetLetters()
    {
        char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        for (int i = 0; i < letters.Length; i++)
        {
            eventDispatcher.Dispatch(new GetLetterEvent(letters[i]));
        }
    }

    public async Task StartGame()
    {
        var response = await _restClientAdapter
               .StartGame<NewGameResponse>(EndPoints.NewGame);
        UpdateToken(response.token);
        eventDispatcher.Dispatch(new GetWordEvent(AddSpacesBetweenLetters(response.hangman)));
    }

    private void UpdateToken(string token)
    {
        _token = token;
    }

    private static string AddSpacesBetweenLetters(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }

    public async void GuessLetter(string letter)
    {
        var response = await
                      _restClientAdapter.GuessLetter<GuessLetterResponse>
                          (EndPoints.GuessLetter, _token, letter);

        UpdateToken(response.token);
        SetGuessResponse(response, letter);
        if (IsCompleted(response.hangman))
        {
            //WIN DISPATCH
            eventDispatcher.Dispatch(new EndEvent(true));
            Debug.Log("Complete");
        }
    }

    private void SetGuessResponse(GuessLetterResponse response, string letter)
    {
        if (response.correct)
        {
            //DISPATCH TO CHANGE LETTER COLOR TO GREEN
            eventDispatcher.Dispatch(new CheckLetterEvent(true, letter));
            _correctLetters.Append($" {letter}");
        }
        else
        {
            //DISPATCH TO LOSE HEALTH
            //DISPATCH TO CHANGE LETTER COLOR TO RED
            eventDispatcher.Dispatch(new CheckLetterEvent(false, letter));
            _incorrectLetters.Append($" {letter}");

        }
        eventDispatcher.Dispatch(new GetWordEvent(AddSpacesBetweenLetters(response.hangman)));
    }

    private async void GetSolution()
    {
        var request = new GetSolutionRequest { token = _token };
        var response =
            await _restClientAdapter.GetSolution<GetSolutionResponse>(EndPoints.GetSolution,
                        _token);

        UpdateToken(response.token);
    }

    private bool IsCompleted(string hangman)
    {
        const string secretCharacter = "_";
        return !hangman.Contains(secretCharacter);
    }
}
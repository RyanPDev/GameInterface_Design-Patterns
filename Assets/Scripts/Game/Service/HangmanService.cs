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

        //_guessLetterButton.onClick.AddListener(GuessLetter);
        //_getSolutionButton.onClick.AddListener(GetSolution);
    }

    //private async void Start()
    //{
    //    await StartGame();
    //}

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
       
        //var request = new NewGameRequest();
        var response = await _restClientAdapter
               .StartGame<NewGameResponse>(EndPoints.NewGame);
        UpdateToken(response.token);
        eventDispatcher.Dispatch(new GetWordEvent(AddSpacesBetweenLetters(response.hangman)));
        //_hangmanText.SetText(AddSpacesBetweenLetters(response.hangman));
    }

    private void UpdateToken(string token)
    {
        _token = token;
        //_tokenText.SetText(_token);
    }

    private static string AddSpacesBetweenLetters(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }

    public async void GuessLetter(string letter)
    {
        ////= _inputField.text;
        //if (string.IsNullOrEmpty(letter))
        //{
        //    Debug.LogError("Input text is null");
        //    return;
        //}

        //if (letter.Length > 1)
        //{
        //    Debug.LogError("Only 1 letter");
        //    return;
        //}

        var request = new GuessLetterRequest { letter = letter, token = _token };
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

            //_correctLettersText.SetText(_correctLetters.ToString());
        }
        else
        {
            //DISPATCH TO LOSE HEALTH
            //DISPATCH TO CHANGE LETTER COLOR TO RED
            eventDispatcher.Dispatch(new CheckLetterEvent(false, letter));
            _incorrectLetters.Append($" {letter}");

            //_incorrectLettersText.SetText(_incorrectLetters.ToString());
        }
        eventDispatcher.Dispatch(new GetWordEvent(AddSpacesBetweenLetters(response.hangman)));
        //_hangmanText.SetText(AddSpacesBetweenLetters(response.hangman));
    }

    private async void GetSolution()
    {
        var request = new GetSolutionRequest { token = _token };
        var response =
            await _restClientAdapter.GetSolution<GetSolutionResponse>(EndPoints.GetSolution,
                        _token);

        UpdateToken(response.token);
        //_hangmanText.SetText(response.solution);
    }

    private bool IsCompleted(string hangman)
    {
        const string secretCharacter = "_";
        return !hangman.Contains(secretCharacter);
    }
}
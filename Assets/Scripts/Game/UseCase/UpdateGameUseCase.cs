using System.Threading.Tasks;

public class UpdateGameUseCase : UseCase, IUpdateGameUseCase
{
    IEventDispatcherService eventDispatcher;
    IHangmanService hangmanService;

    public UpdateGameUseCase(IEventDispatcherService _eventDispatcher, IHangmanService _hangmanService)
    {
        eventDispatcher = _eventDispatcher;
        hangmanService = _hangmanService;
    }

    public void CheckLetter(string letter)
    {
        hangmanService.GuessLetter(letter);
    }

    public async void NewGame()
    {
       await hangmanService.StartGame();

        //eventDispatcher.Dispatch();
    }
}
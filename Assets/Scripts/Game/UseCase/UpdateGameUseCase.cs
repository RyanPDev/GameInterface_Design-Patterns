using System.Threading.Tasks;

public class UpdateGameUseCase : UseCase, IUpdateGameUseCase
{
    IHangmanService hangmanService;

    public UpdateGameUseCase(IHangmanService _hangmanService)
    {
        hangmanService = _hangmanService;
    }

    public void CheckLetter(string letter)
    {
        hangmanService.GuessLetter(letter);

    }

    public async Task NewGame()
    {
        await hangmanService.StartGame();
    }
}
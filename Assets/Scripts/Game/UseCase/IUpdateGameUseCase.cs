using System.Threading.Tasks;

public interface IUpdateGameUseCase
{
    public void CheckLetter(string letter);
    Task NewGame();
}
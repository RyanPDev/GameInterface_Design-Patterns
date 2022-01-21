using System.Threading.Tasks;
public interface IHangmanService
{
    public void GetLetters();
    public Task InitAsync();

    public void GuessLetter(string letter);

    Task StartGame();
}
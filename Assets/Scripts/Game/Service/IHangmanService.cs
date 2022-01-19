using System.Threading.Tasks;
public interface IHangmanService
{
    public void GetLetters();
    public Task InitAsync();
}
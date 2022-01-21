using System.Threading.Tasks;

public interface IUpdateScoreUseCase
{
    void SetScore(int score);
    Task<int> GetScore();
}
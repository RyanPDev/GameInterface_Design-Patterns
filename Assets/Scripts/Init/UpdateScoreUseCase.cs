using System.Threading.Tasks;

public class UpdateScoreUseCase : IUpdateScoreUseCase
{
    readonly IRealTimeDatabaseService realTimeDatabaseService;

    public UpdateScoreUseCase(IRealTimeDatabaseService _realTimeDatabaseService)
    {
        realTimeDatabaseService = _realTimeDatabaseService;
    }

    public void SetScore(int score)
    {
        realTimeDatabaseService.SetData(score);
    }

    public async Task<int> GetScore()
    {
        var aux = await realTimeDatabaseService.GetData();
        return aux;
    }
}
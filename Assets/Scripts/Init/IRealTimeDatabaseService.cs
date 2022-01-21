using System.Threading.Tasks;

public interface IRealTimeDatabaseService
{
    void SetData(int score);
    Task<int> GetData();
}
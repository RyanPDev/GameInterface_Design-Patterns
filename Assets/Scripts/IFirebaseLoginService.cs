public interface IFirebaseLoginService
{
    void Login();
    string GetID();
    bool IDAppExist();
    void SetData();
    void LoadData();
}
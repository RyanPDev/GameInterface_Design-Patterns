public interface IFirebaseLoginService
{
    void Login();
    string GetID();
    void SetData(User user, bool saveInRepo = false);
    void InitUserData();
    void LoadData();
}
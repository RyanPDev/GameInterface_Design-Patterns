public interface IFirebaseLoginService
{
    void Login();
    string GetID();
    void SetData(User user);
    void InitUserData();
    void LoadData();
}
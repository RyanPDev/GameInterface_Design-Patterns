public interface IFirebaseLoginService
{
    void Login();
    string GetID();
    void SetData(User user, bool saveInRepo = false);
    //void InitUserData();
    void UpdateData(UserEntity userEntity);
    void LoadData();
    void SignInIfUserExists(string mail, string password);
}
public interface IFirebaseAccountService
{
    void SignIn(string mail, string password);
    void SignOut();
    void Create(string mail, string password);
    void LoadUserData();
    void SetData(User user);
}
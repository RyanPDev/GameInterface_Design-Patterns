public interface IFirebaseAccountService
{
    void SignIn(string mail, string password);
    void Create(string mail, string password);
    void LoadUserData();
    void SetData(User user);
}
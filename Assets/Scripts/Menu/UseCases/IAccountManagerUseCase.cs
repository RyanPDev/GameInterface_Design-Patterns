public interface IAccountManagerUseCase
{
    void SignIn(string mail, string pass);
    void SignOut();
    void CreateAccount(string mail, string pass);
}
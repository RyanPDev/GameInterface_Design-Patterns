public interface IUserDataAccess
{
    UserEntity GetLocalUser();
    void SetLocalUser(UserEntity userEntity);
}
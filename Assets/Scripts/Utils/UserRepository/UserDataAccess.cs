public interface UserDataAccess
{
    UserEntity GetLocalUser();
    void SetLocalUser(UserEntity userEntity);
}
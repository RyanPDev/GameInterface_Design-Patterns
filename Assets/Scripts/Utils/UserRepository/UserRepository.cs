using System.Collections.Generic;
using UnityEngine;
public class UserRepository : IUserDataAccess
{
    private UserEntity _userEntity;

    public UserEntity GetLocalUser()
    {
        return _userEntity;
    }

    public void SetLocalUser(UserEntity userEntity)
    {
        _userEntity = userEntity;
    }
  //  private void SaveLocalUserOnPlayerPrefs()
  //  {
  //      var json = JsonUtility.ToJson(new )
  //  }
}
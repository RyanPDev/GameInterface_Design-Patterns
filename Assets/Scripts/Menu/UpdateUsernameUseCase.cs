using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UpdateUsernameUseCase : UseCase, IUpdateUsernameUseCase
{
    IUserDataAccess userRepository;
    IEventDispatcherService eventDispatcher;

    public UpdateUsernameUseCase(IUserDataAccess _userRepository, IEventDispatcherService _eventDispatcherService)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcherService;
        eventDispatcher.Subscribe<UserDto>(UpdateOnLogin);
    }
    public void UpdateOnLogin(UserDto userData)
    {
        var userEntity = new UserEntity(userData.Name);
        userRepository.SetLocalUser(userEntity);
    }

    public void UpdateUsername(string userName)
    {
        // Make the Update with a service;
        var userEntity = new UserEntity(userName);
        userRepository.SetLocalUser(userEntity);
        eventDispatcher.Dispatch(userEntity);
    }
    public override void Dispose()
    {
        base.Dispose();
        eventDispatcher.Unsubscribe<UserDto>(UpdateOnLogin);
    }
}
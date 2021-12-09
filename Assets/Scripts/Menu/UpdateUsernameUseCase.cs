using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UpdateUsernameUseCase : IUpdateUsernameUseCase
{
    IUserDataAccess userRepository;
    IEventDispatcherService eventDispatcher;

    public UpdateUsernameUseCase(IUserDataAccess _userRepository, IEventDispatcherService _eventDispatcherService)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcherService;
    }

    public void UpdateUsername(string userName)
    {
        // Make the Update with a service;
        var userEntity = new UserEntity(userName);
        userRepository.SetLocalUser(userEntity);
        eventDispatcher.Dispatch(userEntity);
    }  
}
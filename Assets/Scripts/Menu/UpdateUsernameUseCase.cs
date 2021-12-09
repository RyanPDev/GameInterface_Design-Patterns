using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class UpdateUsernameUseCase : IUpdateUsernameUseCase
{
    UserDataAccess userRepository;
    IEventDispatcherService eventDispatcher;
    public UpdateUsernameUseCase(UserDataAccess _userRepository, IEventDispatcherService _eventDispatcherService)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcherService;
    }

    public void UpdateUsername(string userName)
    {
        // Make the Update with a service;

        var userEntity = new UserEntity("id", userName);
        eventDispatcher.Dispatch(userEntity);
    }
}


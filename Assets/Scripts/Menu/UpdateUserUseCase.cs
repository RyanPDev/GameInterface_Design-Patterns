class UpdateUserUseCase : UseCase, IUpdateUserUseCase
{
    IUserDataAccess userRepository;
    IEventDispatcherService eventDispatcher;

    public UpdateUserUseCase(IUserDataAccess _userRepository, IEventDispatcherService _eventDispatcherService)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcherService;
        eventDispatcher.Subscribe<UserDto>(UpdateOnLogin);
    }
    public void UpdateOnLogin(UserDto userData)
    {
        var userEntity = new UserEntity(userData.Name, userData.Audio, userData.Notifications);
        userRepository.SetLocalUser(userEntity);
    }

    public void UpdateUsername(string userName)
    {
        // Make the Update with a service;
        var userEntity = new UserEntity(userName, userRepository.GetLocalUser().Audio, userRepository.GetLocalUser().Notifications);
        userRepository.SetLocalUser(userEntity);
        eventDispatcher.Dispatch(userEntity);
    }

    public void UpdateAudio(bool audio)
    {
        var userEntity = new UserEntity(userRepository.GetLocalUser().Name, audio, userRepository.GetLocalUser().Notifications);
        userRepository.SetLocalUser(userEntity);
        eventDispatcher.Dispatch(userEntity);
    }

    public void UpdateNotifications(bool notifications)
    {
        var userEntity = new UserEntity(userRepository.GetLocalUser().Name, userRepository.GetLocalUser().Audio, notifications);
        userRepository.SetLocalUser(userEntity);
        eventDispatcher.Dispatch(userEntity);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcher.Unsubscribe<UserDto>(UpdateOnLogin);
    }
}
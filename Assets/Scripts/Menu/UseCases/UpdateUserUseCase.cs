class UpdateUserUseCase : UseCase, IUpdateUserUseCase
{
    readonly IUserDataAccess userRepository;
    readonly IEventDispatcherService eventDispatcher;
    readonly IFirebaseLoginService firebaseLoginService;

    public UpdateUserUseCase(IFirebaseLoginService _firebaseLoginService, IUserDataAccess _userRepository, IEventDispatcherService _eventDispatcherService)
    {
        userRepository = _userRepository;
        eventDispatcher = _eventDispatcherService;
        firebaseLoginService = _firebaseLoginService;
        eventDispatcher.Subscribe<UserInfo>(UpdateOnLogin);
    }

    public void UpdateOnLogin(UserInfo userData)
    {
        var userEntity = new UserEntity(userData.Name, userData.Audio, userData.Notifications);
        userRepository.SetLocalUser(userEntity);
    }

    public void UpdateUsername(string userName)
    {
        // Make the Update with a service;
        var userEntity = new UserEntity(userName, userRepository.GetLocalUser().Audio, userRepository.GetLocalUser().Notifications);
        UpdateInfo(userEntity);
        //userRepository.SetLocalUser(userEntity);
        //firebaseLoginService.UpdateData(userEntity);
        eventDispatcher.Dispatch(userEntity);
    }

    public void UpdateAudio(bool audio)
    {
        var userEntity = new UserEntity(userRepository.GetLocalUser().Name, audio, userRepository.GetLocalUser().Notifications);
        UpdateInfo(userEntity);
        //userRepository.SetLocalUser(userEntity);
        //eventDispatcher.Dispatch(userEntity);
    }

    public void UpdateNotifications(bool notifications)
    {
        var userEntity = new UserEntity(userRepository.GetLocalUser().Name, userRepository.GetLocalUser().Audio, notifications);
        UpdateInfo(userEntity);
        //userRepository.SetLocalUser(userEntity);
        //eventDispatcher.Dispatch(userEntity);

        eventDispatcher.Dispatch(new NotificationsHandler(userEntity.Notifications));
    }

    public void UpdateInfo(UserEntity userEntity)
    {
        userRepository.SetLocalUser(userEntity);
        firebaseLoginService.UpdateData(userEntity);
    }

    public override void Dispose()
    {
        base.Dispose();
        eventDispatcher.Unsubscribe<UserInfo>(UpdateOnLogin);
    }
}
using JetBrains.Annotations;

public class PongMultiplayerMatchMenuUiController : BaseUiController
{
    [UsedImplicitly]
    public void QuitMatch()
    {
        ServiceLocator.Get<NetworkConnectionManager>().Disconnect();
    }
}
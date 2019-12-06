public class PongMultiplayerMatchMenuUiController : BaseUiController
{
    public void QuitMatch()
    {
        ServiceLocator.Get<NetworkConnectionManager>().Disconnect();
    }
}
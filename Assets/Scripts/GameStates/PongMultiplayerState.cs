namespace Assets.Scripts.GameStates
{
    public class PongMultiplayerState : PongState
    {
        protected override PongManager CreatePongManager()
        {
            return ServiceLocator.Register<PongManager>(new PongMultiplayerManager());
        }
    }
}
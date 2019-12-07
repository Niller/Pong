using Assets.Scripts;
using Assets.Scripts.Fsm;
using Assets.Scripts.Signals;

public class PongMultiplayerState : BaseState
{
    private PongManager _pongManager;
    private IInputSystem _inputSystem;

    public override void Enter()
    {
        _inputSystem = ServiceLocator.Get<IInputSystem>();
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.Match, true);

        var difficult = (int)FsmManager.GetBlackboardValue("Difficult");

        _pongManager = ServiceLocator.Register<PongManager>(new PongMultiplayerManager());
        _pongManager.Initialize(ServiceLocator.Get<SettingsManager>().Config.Difficulties[difficult]);
        _pongManager.SpawnPaddles();
        _pongManager.SpawnBall();

        SignalBus.Invoke(new GameStartedSignal(_pongManager.Paddle1, _pongManager.Paddle2));
    }

    public override void Exit()
    {
        _pongManager.Dispose();
        SignalBus.Invoke(new MatchStopSignal());
        ServiceLocator.Get<GuiManager>().CloseAll();
    }

    public override void Execute(float deltaTime)
    {
        _inputSystem.Update(deltaTime);
        _pongManager.Update(deltaTime);
    }
}
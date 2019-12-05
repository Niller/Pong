using Assets.Scripts;
using Assets.Scripts.Fsm;
using Assets.Scripts.Signals;
using UnityEngine;

public class PongMatchState : BaseState
{
    private PongManager _pongManager;

    public override void Enter()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.Match, true);

        var difficult = (int) FsmManager.GetBlackboardValue("Difficult");

        _pongManager = ServiceLocator.Get<PongManager>();
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
        _pongManager.Update(deltaTime);
    }
}
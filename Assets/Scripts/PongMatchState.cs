using Assets.Scripts;
using Assets.Scripts.Fsm;
using Assets.Scripts.Signals;
using UnityEngine;

public class PongMatchState : BaseState
{
    private Ball _ball;
    private PongManager _pongManager;

    public override void Enter()
    {
        _pongManager = ServiceLocator.Get<PongManager>();
        _pongManager.Initialize();
        _pongManager.SpawnPaddles();
        _pongManager.SpawnBall();

        SignalBus.Invoke(new GameStartedSignal(_pongManager.Paddle1, _pongManager.Paddle2));
    }

    public override void Exit()
    {
        
    }

    public override void Execute(float deltaTime)
    {
        _pongManager.Update(deltaTime);
    }
}
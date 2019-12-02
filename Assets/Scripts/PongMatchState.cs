using Assets.Scripts;
using Assets.Scripts.Fsm;
using Assets.Scripts.Signals;

public class PongMatchState : BaseState
{
    private Bat _bat1;
    private Bat _bat2;
    private Ball _ball;
    private Pitch _pitch;

    public override void Enter()
    {
        _pitch = new Pitch();
        _bat1 = new Bat(_pitch);
        _bat2 = new Bat(_pitch);
        _ball = new Ball();

        SignalBus.Invoke(new GameStartedSignal(_bat1, _bat2));
        SignalBus.Invoke(new BallSpawnSignal(_ball));
    }

    public override void Exit()
    {
        
    }

    public override void Execute(float deltaTime)
    {
        _bat1.Update(deltaTime);
        _bat2.Update(deltaTime);
    }
}
using Assets.Scripts.Signals;

namespace Assets.Scripts
{
    public class GameStartedSignal : Signal<Paddle, Paddle>
    {
        public GameStartedSignal(Paddle bat1, Paddle bat2) : base(bat1, bat2)
        {
        }
    }

    public class BallSpawnSignal : Signal<Ball>
    {
        public BallSpawnSignal(Ball ball) : base(ball)
        {
        }
    }

    public class BallDespawnSignal : Signal<Ball>
    {
        public BallDespawnSignal(Ball ball) : base(ball)
        {
        }
    }

    public class MoveInputSignal : Signal<int, float>
    {
        public MoveInputSignal(int direction, float force) : base(direction, force)
        {
        }
    }
}
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

    public class BallHitSignal : Signal
    {
        public BallHitSignal() : base()
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

    public class ScoreChangedSignal : Signal<int>
    {
        public ScoreChangedSignal(int newScore) : base(newScore)
        {
        }
    }

    public class HighscoreChangedSignal : Signal<int>
    {
        public HighscoreChangedSignal(int newScore) : base(newScore)
        {
        }
    }

    public class MatchStopSignal : Signal
    {
        public MatchStopSignal() : base()
        {
        }
    }
}
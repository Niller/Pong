using Assets.Scripts.Signals;

namespace Assets.Scripts
{
    public class GameStartedSignal : Signal<Bat, Bat>
    {
        public GameStartedSignal(Bat bat1, Bat bat2) : base(bat1, bat2)
        {
        }
    }

    public class BallSpawnSignal : Signal<Ball>
    {
        public BallSpawnSignal(Ball ball) : base(ball)
        {
        }
    }
}
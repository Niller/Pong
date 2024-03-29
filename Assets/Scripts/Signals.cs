﻿using Assets.Scripts.Signals;

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

    public class BallHitSignal : Signal<int, float>
    {
        public BallHitSignal(int index, float point) : base(index, point)
        {
        }
    }

    public class BallDespawnSignal : Signal<Ball>
    {
        public BallDespawnSignal(Ball ball) : base(ball)
        {
        }
    }

    public class MoveInputSignal : Signal<int, int, float>
    {
        public MoveInputSignal(int paddle, int direction, float force) : base(paddle, direction, force)
        {
        }
    }

    public class PaddlePositionChangedSignal : Signal<Paddle>
    {
        public PaddlePositionChangedSignal(Paddle paddle) : base(paddle)
        {

        }
    }

    public class BallPositionChangedSignal : Signal<Ball>
    {
        public BallPositionChangedSignal(Ball ball) : base(ball)
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
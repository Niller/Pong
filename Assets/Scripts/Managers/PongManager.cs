using System;
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class PongManager : IDisposable
{
    private const float BounceCoefficient = 0.5f;

    protected GameDifficult GameDifficult;

    public Paddle Paddle1
    {
        get;
        protected set;
    }

    public Paddle Paddle2
    {
        get;
        protected set;
    }

    public Ball Ball
    {
        get;
        protected set;
    }

    public virtual bool IsMultiplayer => false;

    public int Score
    {
        get;
        protected set;
    }

    public void Initialize(GameDifficult gameDifficult)
    {
        SetScore(0);
        GameDifficult = gameDifficult;
    }

    public virtual void SpawnPaddles()
    {
        Paddle1 = new Paddle(0, Vector2.down);
        Paddle2 = new Paddle(1, Vector2.up);
    }

    public virtual bool HandleCollision(Ball ball, out Vector2 newDirection)
    {
        if (!CheckCollision(ball, out var relativeIntersect, out var currentPaddle))
        {
            newDirection = ball.Direction;
            return false;
        }

        SetScore(Score + 1);

        SignalBus.Invoke(new BallHitSignal(currentPaddle.Index, relativeIntersect));
        newDirection = new Vector2(relativeIntersect * BounceCoefficient, -ball.Direction.y).normalized;

        return true;
    }

    public virtual void SpawnBall()
    {
        Ball = new Ball(
            Vector2.zero,
            GameDifficult.BallSize, 
            GameDifficult.InitialBallSpeed, 
            GameDifficult.BallSpeedIncrement);

        SignalBus.Invoke(new BallSpawnSignal(Ball));
        
    }

    public virtual void DespawnBall()
    {
        ServiceLocator.Get<SettingsManager>().TrySaveHighscore(Score);
        SignalBus.Invoke(new HighscoreChangedSignal(Score));
        SetScore(0);

        SignalBus.Invoke(new BallDespawnSignal(Ball));
        SpawnBall();
    }

    public virtual void Update(float deltaTime)
    {
        Ball?.Update(deltaTime);
    }

    public void Dispose()
    {
        Ball = null;
        Paddle1 = null;
        Paddle2 = null;
        SetScore(0);
    }

    protected virtual void SetScore(int value)
    {
        Score = value;
        SignalBus.Invoke(new ScoreChangedSignal(Score));
    }

    protected bool CheckCollision(Ball ball, out float relativeIntersect, out Paddle currentPaddle)
    {
        currentPaddle = ball.Position.y < 0 ? Paddle1 : Paddle2;
        var paddleLength = currentPaddle.Length;
        var paddlePosition = currentPaddle.Position.x;
        var ballPosition = ball.Position.x;

        //[-1, 1]
        relativeIntersect = (ballPosition - paddlePosition) / paddleLength;
        if (relativeIntersect < -1 || relativeIntersect > 1)
        {
            return false;
        }

        return true;
    }
}
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class PongManager
{
    private const float BounceCoefficient = 0.5f;

    public Paddle Paddle1
    {
        get;
        private set;
    }

    public Paddle Paddle2
    {
        get;
        private set;
    }

    public Ball Ball
    {
        get;
        private set;
    }

    public void SpawnPaddles()
    {
        Paddle1 = new Paddle(Vector2.down);
        Paddle2 = new Paddle(Vector2.up);
    }

    public bool CheckCollision(Ball ball, out Vector2 newDirection)
    {
        var currentPaddle = ball.Position.y < 0 ? Paddle1 : Paddle2;
        var paddleLength = currentPaddle.Length;
        var paddlePosition = currentPaddle.Position.x;
        var ballPosition = ball.Position.x;

        //[-1, 1]
        var relativeIntersect = (ballPosition - paddlePosition) / paddleLength;
        if (relativeIntersect < -1 || relativeIntersect > 1)
        {
            newDirection = ball.Direction;
            return false;
        }

        currentPaddle.OnBallHit(relativeIntersect);
        newDirection = new Vector2(relativeIntersect * BounceCoefficient, -ball.Direction.y).normalized;
        return true;
    }

    public void SpawnBall()
    {
        Ball = new Ball(Vector2.zero);
        SignalBus.Invoke(new BallSpawnSignal(Ball));
    }

    public void DespawnBall()
    {
        SignalBus.Invoke(new BallDespawnSignal(Ball));
        SpawnBall();
    }

    public void Update(float deltaTime)
    {
        Paddle1.Update(deltaTime);
        Paddle2.Update(deltaTime);
        Ball.Update(deltaTime);
    }
}
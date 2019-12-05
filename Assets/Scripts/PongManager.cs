using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class PongManager
{
    private const float BounceCoefficient = 0.5f;

    private int _score;
    private GameDifficult _gameDifficult;

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

    public bool IsMultiplayer
    {
        get;
        private set;
    }

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            SignalBus.Invoke(new ScoreChangedSignal(Score));
        }
    }

    public void Initialize(GameDifficult gameDifficult, bool isMultiplayer)
    {
        IsMultiplayer = isMultiplayer;
        Score = 0;
        _gameDifficult = gameDifficult;
    }

    public void SpawnPaddles()
    {
        Paddle1 = new Paddle(0, Vector2.down);
        Paddle2 = new Paddle(1, Vector2.up);
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

        Score++;

        SignalBus.Invoke(new BallHitSignal());
        currentPaddle.OnBallHit(relativeIntersect);
        newDirection = new Vector2(relativeIntersect * BounceCoefficient, -ball.Direction.y).normalized;
        return true;
    }

    public void SpawnBall()
    {
        Ball = new Ball(
            Vector2.zero,
            _gameDifficult.BallSize, 
            _gameDifficult.InitialBallSpeed, 
            _gameDifficult.BallSpeedIncrement);

        SignalBus.Invoke(new BallSpawnSignal(Ball));
    }

    public void DespawnBall()
    {
        ServiceLocator.Get<SettingsManager>().TrySaveHighscore(Score);
        SignalBus.Invoke(new HighscoreChangedSignal(Score));
        Score = 0;

        SignalBus.Invoke(new BallDespawnSignal(Ball));
        SpawnBall();
    }

    public void Update(float deltaTime)
    {
        Paddle1.Update(deltaTime);
        Paddle2.Update(deltaTime);
        Ball.Update(deltaTime);
    }

    public void Dispose()
    {
        Ball = null;
        Paddle1 = null;
        Paddle2 = null;
        Score = 0;
    }
}
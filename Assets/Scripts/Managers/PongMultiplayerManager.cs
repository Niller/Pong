using Assets.Scripts;
using Assets.Scripts.Signals;
using Photon.Pun;
using UnityEngine;

public class PongMultiplayerManager : PongManager
{
    private NetworkGameManager _networkGameManager;
    public override bool IsMultiplayer => true;
    public bool IsHost => PhotonNetwork.IsMasterClient;

    public override void Initialize(GameDifficult gameDifficult)
    {
        _networkGameManager = ServiceLocator.Get<NetworkGameManager>();
        base.Initialize(gameDifficult);
    }

    public override bool HandleCollision(Ball ball, out Vector2 newDirection)
    {
        newDirection = ball.Direction;

        if (!CheckCollision(ball, out var relativeIntersect, out var currentPaddle))
        {
            return false;
        }

        if (!IsHost)
        {
            return true;
        }

        var result = base.HandleCollision(ball, out newDirection);
        if (result)
        {
            _networkGameManager.CallPaddleHitBall(currentPaddle.Index, relativeIntersect);
        }
        return result;
    }

    public override void SpawnPaddles()
    {
        Paddle1 = new PaddleMultiplayer(0, Vector2.down);
        Paddle2 = new PaddleMultiplayer(1, Vector2.up);
    }

    public override void SpawnBall()
    {
        if (!IsHost)
        {
            return;
        }

        SpawnBallInternal();
    }

    public override void DespawnBall()
    {
        if (!IsHost)
        {
            return;
        }

        base.DespawnBall();
    }

    public void SyncBall(Vector2 position, Vector2 direction, float speed)
    {
        if (Ball == null)
        {
            SpawnBallInternal();
        }
        ((BallMultiplayer)Ball).Sync(position, direction, speed);
    }

    public void SyncScore(int score)
    {
        SetScore(score);
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (IsHost)
        {
            _networkGameManager.CallSyncBall(Ball);
        }
    }


    protected override void SetScore(int value)
    {
        base.SetScore(value);
        if (IsHost)
        {
            _networkGameManager.CallSyncScore(value);
        }
    }

    private void SpawnBallInternal()
    {
        Ball = new BallMultiplayer(
            Vector2.zero,
            GameDifficult.BallSize,
            GameDifficult.InitialBallSpeed,
            GameDifficult.BallSpeedIncrement);

        SignalBus.Invoke(new BallSpawnSignal(Ball));
    }
}
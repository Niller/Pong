using Assets.Scripts;
using Assets.Scripts.Signals;
using Photon.Pun;
using UnityEngine;

public class PongMultiplayerManager : PongManager
{
    public override bool IsMultiplayer => true;
    public bool IsHost => PhotonNetwork.IsMasterClient;

    public override bool HandleCollision(Ball ball, out Vector2 newDirection)
    {
        newDirection = ball.Direction;

        if (!CheckCollision(ball, out _, out _))
        {
            return false;
        }

        if (!IsHost)
        {
            return true;
        }

        return base.HandleCollision(ball, out newDirection);
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

        Ball = new BallMultiplayer(
            Vector2.zero,
            GameDifficult.BallSize,
            GameDifficult.InitialBallSpeed,
            GameDifficult.BallSpeedIncrement);

        SignalBus.Invoke(new BallSpawnSignal(Ball));
    }

    public override void DespawnBall()
    {
        if (!IsHost)
        {
            return;
        }

        base.DespawnBall();
    }

    protected override void SetScore(int value)
    {
        base.SetScore(value);
        if (IsMultiplayer && IsHost)
        {
            ServiceLocator.Get<NetworkGameManager>().CallSyncScore(value);
        }
    }
}
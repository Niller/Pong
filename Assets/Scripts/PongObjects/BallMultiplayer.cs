using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class BallMultiplayer : Ball
{
    private readonly PongMultiplayerManager _pongMultiplayerManager;

    public BallMultiplayer(Vector2 startPosition, float size, float speed, float speedIncrement) : base(startPosition, size, speed,
        speedIncrement)
    {
        _pongMultiplayerManager = (PongMultiplayerManager) PongManager;
    }

    public override void Update(float deltaTime)
    {
        if (!_pongMultiplayerManager.IsHost)
        {
            return;
        }

        base.Update(deltaTime);
    }

    public void Sync(Vector2 position, Vector2 direction, float speed)
    {
        Position = position;
        Direction = direction;
        Speed = speed;

        SignalBus.Invoke(new BallPositionChangedSignal(this));
    }

}
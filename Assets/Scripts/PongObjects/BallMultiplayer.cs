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
}
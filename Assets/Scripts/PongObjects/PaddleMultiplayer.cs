using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class PaddleMultiplayer : Paddle
{
    private readonly PongMultiplayerManager _pongMultiplayerManager;

    public PaddleMultiplayer(int index, Vector2 startPosition) : base(index, startPosition)
    {
        _pongMultiplayerManager = (PongMultiplayerManager) ServiceLocator.Get<PongManager>();
    }

    public void Sync(float pos)
    {
        Position = new Vector2(pos, Position.y);
        SignalBus.Invoke(new PaddlePositionChangedSignal(this));
    }

    protected override void OnMoveInput(MoveInputSignal data)
    {
        if (!_pongMultiplayerManager.IsHost && Index == 0)
        {
            return;
        }

        base.OnMoveInput(data);

        if (data.Arg1 != Index)
        {
            return;
        }

        if (_pongMultiplayerManager.IsHost && Index == 1)
        {
            ServiceLocator.Get<NetworkGameManager>().CallSyncPaddle(this);
        }
    }
}
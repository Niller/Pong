﻿using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class PaddleMultiplayer : Paddle
{
    private readonly PongMultiplayerManager _pongMultiplayerManager;
    private NetworkGameManager _networkGameManager;

    public PaddleMultiplayer(int index, Vector2 startPosition) : base(index, startPosition)
    {
        _pongMultiplayerManager = (PongMultiplayerManager) ServiceLocator.Get<PongManager>();
        _networkGameManager = ServiceLocator.Get<NetworkGameManager>();
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
            _networkGameManager.CallSyncPaddle(this);
        }
    }
}

using System;
using Assets.Scripts;
using Assets.Scripts.Signals;
using Photon.Pun;
using UnityEngine;

public class Paddle : PongObject
{
    public float Length
    {
        get;
        private set;
    }

    public event Action<float> BallHit;

    private readonly int _index;

    public Paddle(int index, Vector2 startPosition)
    {
        _index = index;
        Position = startPosition;
        Length = 0.2f;
        SignalBus.Subscribe<MoveInputSignal>(OnMoveInput);
    }

    private void OnMoveInput(MoveInputSignal data)
    {
        if (ServiceLocator.Get<PongManager>().IsMultiplayer && !PhotonNetwork.IsMasterClient && _index == 0)
        {
            return;
        }

        if (data.Arg1 != _index)
        {
            return;
        }

        Position = new Vector2(Mathf.Clamp(Position.x + data.Arg2 * data.Arg3, -1f + Length, 1f - Length), Position.y);
        SignalBus.Invoke(new PaddlePositionChangedSignal(this));
        if (ServiceLocator.Get<PongManager>().IsMultiplayer && PhotonNetwork.IsMasterClient && _index == 1)
        {
            ServiceLocator.Get<NetworkGameManager>().CallSyncPaddle(this);
        }
    }

    public void OnBallHit(float relativeHitPosition)
    {
        BallHit?.Invoke(relativeHitPosition);
    }

    public void Sync(float pos)
    {
        Position = new Vector2(pos, Position.y);
        SignalBus.Invoke(new PaddlePositionChangedSignal(this));
    }
}
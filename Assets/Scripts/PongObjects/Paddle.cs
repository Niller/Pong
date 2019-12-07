
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
    }

    public int Index
    {
        get;
    }

    public Paddle(int index, Vector2 startPosition)
    {
        Index = index;
        Position = startPosition;
        Length = 0.2f;
        SignalBus.Subscribe<MoveInputSignal>(OnMoveInput);
    }

    protected virtual void OnMoveInput(MoveInputSignal data)
    {
        if (data.Arg1 != Index)
        {
            return;
        }

        Position = new Vector2(Mathf.Clamp(Position.x + data.Arg2 * data.Arg3, -1f + Length, 1f - Length), Position.y);
        SignalBus.Invoke(new PaddlePositionChangedSignal(this));
    }

    public void OnBallHit(float relativeHitPosition)
    {
    }
}
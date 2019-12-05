
using System;
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class Paddle
{
    public Vector2 Position
    {
        get;
        private set;
    }

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
        if (data.Arg1 != _index)
        {
            return;
        }

        Position = new Vector2(Mathf.Clamp(Position.x + data.Arg2 * data.Arg3, -1f + Length, 1f - Length), Position.y);
    }

    public void OnBallHit(float relativeHitPosition)
    {
        BallHit?.Invoke(relativeHitPosition);
    }

    public void Update(float deltaTime)
    {

        /*
        Position += deltaTime * _sign;
        if (Position >= 1 || Position <= -1)
        {
            _sign *= -1;
        }
        */
    }
}
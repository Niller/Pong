
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

    public Paddle(Vector2 startPosition)
    {
        Position = startPosition;
        Length = 0.2f;
        SignalBus.Subscribe<MoveInputSignal>(OnMoveInput);
    }

    private void OnMoveInput(MoveInputSignal data)
    {
        Position = new Vector2(Mathf.Clamp(Position.x + data.Arg1 * data.Arg2, -1f + Length, 1f - Length), Position.y);
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

using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class Bat
{
    private readonly Pitch _pitch;

    public Vector2 Position;
    public float Length;

    private float _sign = 1;

    public Bat(Pitch pitch, Vector2 startPosition)
    {
        _pitch = pitch;
        Position = startPosition;
        Length = 0.5f;
        SignalBus.Subscribe<MoveInputSignal>(OnMoveInput);
    }

    private void OnMoveInput(MoveInputSignal data)
    {
        Position = new Vector2(Mathf.Clamp(Position.x + data.Arg1 * data.Arg2, -1f + Length, 1f - Length), Position.y);
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
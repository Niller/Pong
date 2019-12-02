
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class Bat
{
    private readonly Pitch _pitch;

    public Vector2 Position;

    private float _sign = 1;

    public Bat(Pitch pitch, Vector2 startPosition)
    {
        _pitch = pitch;
        Position = startPosition;
        SignalBus.Subscribe<MoveInputSignal>(OnMoveInput);
    }

    private void OnMoveInput(MoveInputSignal data)
    {
        Position = new Vector2(Mathf.Clamp(Position.x + data.Arg1 * data.Arg2, -1, 1), Position.y);
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
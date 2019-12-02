
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class Bat
{
    private readonly Pitch _pitch;

    public float Position;

    private float _sign = 1;

    public Bat(Pitch pitch)
    {
        _pitch = pitch;
        Position = _pitch.BatStartPosition;
        SignalBus.Subscribe<MoveInputSignal>(OnMoveInput);
    }

    private void OnMoveInput(MoveInputSignal data)
    {
        Position = Mathf.Clamp(Position + data.Arg1 * data.Arg2, -1, 1);
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
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class Paddle : PongObject
{
    private const float DefaultLength = 0.2f;

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
        Length = DefaultLength;
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

}
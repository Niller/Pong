using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public abstract class PongObjectView : MonoBehaviour
{
    private int _lerpFrameCount = 3;
    private int _currentFrameCount;
    private Vector2? _previousNextPosition;

    protected Transform Transform;
    protected Vector2 PitchSize;

    private Vector2 _nextPosition;
    protected Vector2 NextPosition
    {
        get => _nextPosition;
        set
        {
            _previousNextPosition = _nextPosition;
            _nextPosition = value;
            _currentFrameCount = 0;
        }
    }

    public void Initialize(Vector2 pitchSize, PongObject pongObject)
    {
        PitchSize = pitchSize;
        Transform = transform;
    }

    protected virtual void Update()
    {
        if (_currentFrameCount > _lerpFrameCount)
        {
            return;
        }

        Vector2 pos;

        if (!_previousNextPosition.HasValue)
        {
            pos = new Vector2(Transform.position.x, Transform.position.y);
            _nextPosition = pos;
            _currentFrameCount = int.MaxValue;
        }
        else
        {
            pos = Vector2.Lerp(_previousNextPosition.Value, NextPosition, (_currentFrameCount++ / (float)_lerpFrameCount));
        }

        Transform.position = new Vector3(
            pos.x,
            pos.y,
            Transform.localPosition.z);
    }
}
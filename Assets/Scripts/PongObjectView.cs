using System;
using UnityEngine;

public abstract class PongObjectView : MonoBehaviour
{
    protected Transform Transform;
    protected Vector2 PitchSize;
    protected float? NextPosition;
    private float _lerpDelta = 0.1f;
    private PongObject _pongObject;

    public void Initialize(Vector2 pitchSize, PongObject pongObject)
    {
        PitchSize = pitchSize;
        Transform = transform;
        _pongObject = pongObject;
    }

    protected virtual void Update()
    {
        if (NextPosition == null)
        {
            return;
        }

        var direction = Mathf.Sign(NextPosition.Value - Transform.position.x);
        var newPos = direction < 0
            ? Mathf.Max(Transform.position.x + direction * _lerpDelta, NextPosition.Value)
            : Mathf.Min(Transform.position.x + direction * _lerpDelta, NextPosition.Value);

        Transform.position = new Vector3(
            newPos,
            _pongObject.Position.y * (PitchSize.y / 2f),
            Transform.localPosition.z);

        if (Math.Abs(newPos - NextPosition.Value) < float.Epsilon)
        {
            NextPosition = null;
        }

    }
}
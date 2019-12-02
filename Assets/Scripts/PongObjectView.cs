using UnityEngine;

public abstract class PongObjectView : MonoBehaviour
{
    protected Transform Transform;
    protected Vector2 PitchSize;

    public void Initialize(Vector2 pitchSize)
    {
        PitchSize = pitchSize;
        Transform = transform;
    }
}
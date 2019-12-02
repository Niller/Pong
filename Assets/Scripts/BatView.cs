using UnityEngine;

public class BatView : MonoBehaviour
{
    private Bat _bat;
    private Transform _transform;
    private Vector2 _pitchSize;
    private float _yPosition;

    public void Initialize(Bat bat, Vector2 pitchSize, float yPosition)
    {
        _bat = bat;
        _pitchSize = pitchSize;
        _yPosition = yPosition;
        _transform = transform;
    }

    private void Update()
    {
        _transform.localPosition = new Vector3(_bat.Position * (_pitchSize.x/2f), _yPosition, _transform.localPosition.z);
    }
}
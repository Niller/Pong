using UnityEngine;

public class BatView : PongObjectView
{
    private Bat _bat;
    private float _yPosition;

    [SerializeField]
    private GameObject _view;

    public void Initialize(Bat bat, Vector2 pitchSize, float yPosition)
    {
        base.Initialize(pitchSize);
        _bat = bat;
        _yPosition = yPosition;

        var viewTransform = _view.transform;
        viewTransform.localScale = new Vector3(_bat.Length * pitchSize.x, viewTransform.localScale.y, viewTransform.localScale.z);
    }

    private void Update()
    {
        Transform.localPosition = new Vector3(
            _bat.Position.x * (PitchSize.x / 2f),
            _bat.Position.y * (PitchSize.y / 2f),
            Transform.localPosition.z);
    }
}
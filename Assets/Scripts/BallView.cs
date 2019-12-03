using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class BallView : PongObjectView
{
    private Ball _ball;

    [SerializeField]
    private GameObject _view;

    private Vector3 _rotationSpeed;
    private Transform _viewTransform;

    public void Initialize(Ball ball, Vector2 pitchSize)
    {
        base.Initialize(pitchSize);
        _ball = ball;

        _viewTransform = _view.transform;
        var radius = _ball.Size * pitchSize.y;
        _viewTransform.localScale = new Vector3(radius, radius, radius);

        SignalBus.Subscribe<BallHitSignal>(OnBallHit);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<BallHitSignal>(OnBallHit);
    }

    private void OnBallHit(BallHitSignal data)
    {
        _rotationSpeed = new Vector3(Random.value, Random.value, Random.value) * Random.Range(2, 5);
    }

    private void Update()
    {
        Transform.localPosition = new Vector3(
            _ball.Position.x * (PitchSize.x / 2f),
            _ball.Position.y * (PitchSize.y / 2f), 
            Transform.localPosition.z);

        _viewTransform.Rotate(_rotationSpeed);
    }
}
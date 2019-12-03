using UnityEngine;

public class PaddleView : PongObjectView
{
    private Paddle _paddle;
    private float _yPosition;
    private PaddleTiltTweener _tiltTweener;

    [SerializeField]
    private GameObject _view;

    public void Initialize(Paddle paddle, Vector2 pitchSize, float yPosition)
    {
        base.Initialize(pitchSize);
        _paddle = paddle;
        _yPosition = yPosition;

        var viewTransform = _view.transform;
        viewTransform.localScale = new Vector3(_paddle.Length * pitchSize.x, viewTransform.localScale.y, viewTransform.localScale.z);

        _tiltTweener = GetComponent<PaddleTiltTweener>();

        _paddle.BallHit += PaddleOnBallHit;
    }

    private void OnDestroy()
    {
        _paddle.BallHit -= PaddleOnBallHit;
    }

    private void PaddleOnBallHit(float relativeHitPosition)
    {
        _tiltTweener.Run(relativeHitPosition);
    }

    private void Update()
    {
        Transform.localPosition = new Vector3(
            _paddle.Position.x * (PitchSize.x / 2f),
            _paddle.Position.y * (PitchSize.y / 2f),
            Transform.localPosition.z);
    }
}
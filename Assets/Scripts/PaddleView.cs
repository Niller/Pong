using System;
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class PaddleView : PongObjectView
{
    private Paddle _paddle;
    private PaddleTiltTweener _tiltTweener;

    [SerializeField]
    private GameObject _view;

    public void Initialize(Paddle paddle, Vector2 pitchSize)
    {
        base.Initialize(pitchSize, paddle);
        _paddle = paddle;
        Transform.position = new Vector3(
            _paddle.Position.x * (PitchSize.x / 2f),
            _paddle.Position.y * (PitchSize.y / 2f),
            Transform.localPosition.z);
        ;

        var viewTransform = _view.transform;
        viewTransform.localScale = new Vector3(_paddle.Length * pitchSize.x, viewTransform.localScale.y, viewTransform.localScale.z);

        _tiltTweener = GetComponent<PaddleTiltTweener>();
        SignalBus.Subscribe<PaddlePositionChangedSignal>(OnPositionChanged);

        _paddle.BallHit += PaddleOnBallHit;
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<PaddlePositionChangedSignal>(OnPositionChanged);
        _paddle.BallHit -= PaddleOnBallHit;
    }

    private void PaddleOnBallHit(float relativeHitPosition)
    {
        _tiltTweener.Run(relativeHitPosition);
    }

    private void OnPositionChanged(PaddlePositionChangedSignal data)
    {
        if (data.Arg1 == _paddle)
        {
            NextPosition = _paddle.Position.x * (PitchSize.x / 2f);
        }
    }

    
}
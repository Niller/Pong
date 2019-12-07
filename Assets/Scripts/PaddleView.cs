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
        SignalBus.Subscribe<BallHitSignal>(OnBallHit);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<PaddlePositionChangedSignal>(OnPositionChanged);
        SignalBus.Unsubscribe<BallHitSignal>(OnBallHit);
    }

    private void OnBallHit(BallHitSignal data)
    {
        if (_paddle.Index != data.Arg1)
        {
            return;
        }

        _tiltTweener.Run(data.Arg2);
    }

    private void OnPositionChanged(PaddlePositionChangedSignal data)
    {
        if (data.Arg1 == _paddle)
        {
            NextPosition = new Vector2(
                _paddle.Position.x * (PitchSize.x / 2f),
                _paddle.Position.y * (PitchSize.y / 2f));
        }
    }

    
}
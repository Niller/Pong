﻿using UnityEngine;

public class BallView : PongObjectView
{
    private Ball _ball;

    [SerializeField]
    private GameObject _view;

    public void Initialize(Ball ball, Vector2 pitchSize)
    {
        base.Initialize(pitchSize);
        _ball = ball;

        var viewTransform = _view.transform;
        var radius = _ball.Size * pitchSize.y;
        viewTransform.localScale = new Vector3(radius, radius, radius);
    }

    private void Update()
    {
        Transform.localPosition = new Vector3(
            _ball.Position.x * (PitchSize.x / 2f),
            _ball.Position.y * (PitchSize.y / 2f), 
            Transform.localPosition.z);
    }
}
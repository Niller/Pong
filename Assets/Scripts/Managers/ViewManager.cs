﻿using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private ViewConfig _config;

    private readonly Dictionary<object, GameObject> _views = new Dictionary<object, GameObject>();
    private Vector2 _pitchSize;

    public void Initialize(ViewConfig viewConfig)
    {
        _config = viewConfig;
        var orthographicSize = Camera.main.orthographicSize;
        var aspectRatio = (float)Screen.width / Screen.height;
        _pitchSize = new Vector2(aspectRatio * (orthographicSize * 2f), orthographicSize * 2f - _config.PaddleYOffset * 2f);

        SignalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        SignalBus.Subscribe<BallSpawnSignal>(OnBallSpawn);
        SignalBus.Subscribe<BallDespawnSignal>(OnBallDespawn);
        SignalBus.Subscribe<MatchStopSignal>(OnMatchStop);
    }

    private void OnDestroy()
    {
        _views.Clear();
        SignalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        SignalBus.Unsubscribe<BallSpawnSignal>(OnBallSpawn);
        SignalBus.Unsubscribe<BallDespawnSignal>(OnBallDespawn);
        SignalBus.Unsubscribe<MatchStopSignal>(OnMatchStop);
    }

    private void OnGameStarted(GameStartedSignal data)
    {
        var paddle1GameObject = Instantiate(_config.BatView);
        var paddle1View = paddle1GameObject.GetComponent<PaddleView>();
        paddle1View.Initialize(data.Arg1, _pitchSize);
        _views.Add(data.Arg1, paddle1GameObject);

        var paddle2GameObject = Instantiate(_config.BatView);
        var paddle2View = paddle2GameObject.GetComponent<PaddleView>();
        paddle2View.Initialize(data.Arg2, _pitchSize);
        _views.Add(data.Arg2, paddle2GameObject);
    }

    private void OnBallSpawn(BallSpawnSignal data)
    {
        var ballGameObject = Instantiate(_config.BallView);
        var ballView = ballGameObject.GetComponent<BallView>();
        ballView.Initialize(data.Arg1, _pitchSize);
        _views.Add(data.Arg1, ballGameObject);
    }

    private void OnBallDespawn(BallDespawnSignal data)
    {
        if (_views.TryGetValue(data.Arg1, out var ballGameObject))
        {
            Destroy(ballGameObject);
            _views.Remove(data.Arg1);
        }
    }

    private void OnMatchStop(MatchStopSignal data)
    {
        foreach (var view in _views.Values)
        {
            Destroy(view);
        }
        _views.Clear();
    }
}
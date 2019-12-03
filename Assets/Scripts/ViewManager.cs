using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private ViewConfig _config;

    private readonly Dictionary<object, GameObject> _views = new Dictionary<object, GameObject>();

    public void Initialize(ViewConfig viewConfig)
    {
        _config = viewConfig;

        SignalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        SignalBus.Subscribe<BallSpawnSignal>(OnBallSpawn);
        SignalBus.Subscribe<BallDespawnSignal>(OnBallDespawn);
    }

    private void OnDestroy()
    {
        _views.Clear();
        SignalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        SignalBus.Unsubscribe<BallSpawnSignal>(OnBallSpawn);
        SignalBus.Unsubscribe<BallDespawnSignal>(OnBallDespawn);
    }

    private void OnGameStarted(GameStartedSignal data)
    {
        var paddle1GameObject = Instantiate(_config.BatView);
        var paddle1View = paddle1GameObject.GetComponent<PaddleView>();
        paddle1View.Initialize(data.Arg1, _config.PitchSize, _config.BottomBatPosition);
        _views.Add(data.Arg1, paddle1GameObject);

        var paddle2GameObject = Instantiate(_config.BatView);
        var paddle2View = paddle2GameObject.GetComponent<PaddleView>();
        paddle2View.Initialize(data.Arg2, _config.PitchSize, _config.TopBatPosition);
        _views.Add(data.Arg2, paddle2GameObject);
    }

    private void OnBallSpawn(BallSpawnSignal data)
    {
        var ballGameObject = Instantiate(_config.BallView);
        var ballView = ballGameObject.GetComponent<BallView>();
        ballView.Initialize(data.Arg1, _config.PitchSize);
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
}
using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private ViewConfig _config;

    public void Initialize(ViewConfig viewConfig)
    {
        _config = viewConfig;
        SignalBus.Subscribe<GameStartedSignal>(OnGameStarted);
        SignalBus.Subscribe<BallSpawnSignal>(OnBallSpawn);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
        SignalBus.Unsubscribe<BallSpawnSignal>(OnBallSpawn);
    }

    private void OnGameStarted(GameStartedSignal data)
    {
        var bat1GameObject = Instantiate(_config.BatView);
        var bat1View = bat1GameObject.GetComponent<BatView>();
        bat1View.Initialize(data.Arg1);
    }

    private void OnBallSpawn(BallSpawnSignal data)
    {

    }
}
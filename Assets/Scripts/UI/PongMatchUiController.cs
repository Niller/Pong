using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

public class PongMatchUiController : MonoBehaviour
{
    [SerializeField]
    private Text _maxScoreText;

    [SerializeField]
    private Text _currentScoreText;

    private void Awake()
    {
        SignalBus.Subscribe<ScoreChangedSignal>(OnGameStarted);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<ScoreChangedSignal>(OnGameStarted);
    }


    private void OnGameStarted(ScoreChangedSignal data)
    {
        UpdateValues();
    }

    private void UpdateValues()
    {
        var pongManager = ServiceLocator.Get<PongManager>();
        _currentScoreText.text = pongManager.Score.ToString();
    }

}
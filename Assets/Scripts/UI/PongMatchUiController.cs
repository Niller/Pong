﻿using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;

public class PongMatchUiController : BaseUiController
{
#pragma warning disable 649
    [SerializeField]
    private Text _maxScoreText;

    [SerializeField]
    private Text _currentScoreText;
#pragma warning restore 649

    private void Awake()
    {
        SignalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        SignalBus.Subscribe<HighscoreChangedSignal>(OnHighscoreScoreChangedSignal);
        SignalBus.Subscribe<GameStartedSignal>(OnGameStarted);
    }

    private void OnDestroy()
    {
        SignalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
        SignalBus.Unsubscribe<HighscoreChangedSignal>(OnHighscoreScoreChangedSignal);
        SignalBus.Unsubscribe<GameStartedSignal>(OnGameStarted);
    }

    private void OnGameStarted(GameStartedSignal data)
    {
        UpdateScore();
        UpdateHighscore();
    }

    private void OnScoreChanged(ScoreChangedSignal data)
    {
        UpdateScore();
    }

    private void OnHighscoreScoreChangedSignal(HighscoreChangedSignal data)
    {
        UpdateHighscore();
    }

    private void UpdateScore()
    {
        var pongManager = ServiceLocator.Get<PongManager>();
        _currentScoreText.text = pongManager.Score.ToString();
    }

    private void UpdateHighscore()
    {
        _maxScoreText.text = ServiceLocator.Get<SettingsManager>().LoadHighscore().ToString();
    }

    public void OpenMenu()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.MatchMenu);
    }
}
using UnityEngine;

public class SettingsManager
{
    private const string BallColor1Key = "BallColor1_key";
    private const string BallColor2Key = "BallColor2_key";
    private const string HighScoreKey = "HighScore_key";

    public GameConfig Config
    {
        get;
        private set;
    }
    public void Initialize(GameConfig config)
    {
        Config = config;
    }

    public void SaveScore(int value)
    {
        PlayerPrefs.SetInt(HighScoreKey, value);
    }

    public void SaveBallColor(int color1, int color2)
    {
        PlayerPrefs.SetInt(BallColor1Key, color1);
        PlayerPrefs.SetInt(BallColor2Key, color2);
    }

    public int LoadScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }

    public (int, int) LoadBallColor()
    {
        return (PlayerPrefs.GetInt(BallColor1Key, 0), PlayerPrefs.GetInt(BallColor2Key, 1));
    }

}
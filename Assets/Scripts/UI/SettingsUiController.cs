using UnityEngine;
using UnityEngine.UI;

public class SettingsUiController : MonoBehaviour
{
    [SerializeField]
    private Dropdown _color1Dropdown;
    [SerializeField]
    private Dropdown _color2Dropdown;

    private SettingsManager _settingsManager;

    private void Awake()
    {
        _settingsManager = ServiceLocator.Get<SettingsManager>();
        FillColorDropdown(_color1Dropdown);
        FillColorDropdown(_color2Dropdown);
    }

    private void OnEnable()
    {
        var ballColors = _settingsManager.LoadBallColor();
        _color1Dropdown.value = ballColors.Item1;
        _color2Dropdown.value = ballColors.Item2;
    }

    private void FillColorDropdown(Dropdown dropdown)
    {
        dropdown.options.Clear();
        foreach (var ballColor in _settingsManager.Config.BallColors)
        {
            dropdown.options.Add(new Dropdown.OptionData(ballColor.Name, ballColor.Sprite));
        }
    }

    public void SaveSettings()
    {
        _settingsManager.SaveBallColor(_color1Dropdown.value, _color2Dropdown.value);
    }
}
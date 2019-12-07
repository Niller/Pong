using Assets.Scripts.GUI;
using JetBrains.Annotations;
using UnityEngine;

public class MainUiController : BaseUiController
{
    [UsedImplicitly]
    public void StartSingleGame()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.StartGame);
    }

    [UsedImplicitly]
    public void StartMultiplayerGame()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.StartMultiplayerGame);
    }

    [UsedImplicitly]
    public void OpenSettings()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.Settings);
    }

    [UsedImplicitly]
    public void Quit()
    {
        Application.Quit();
    }
}
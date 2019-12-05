using UnityEngine;

public class MainUiController : BaseUiController
{
    public void StartSingleGame()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.StartGame);
    }

    public void OpenSettings()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.Settings);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
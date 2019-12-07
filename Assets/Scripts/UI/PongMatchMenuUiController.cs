using Assets.Scripts.Framework.Fsm;
using Assets.Scripts.GameStates;
using Assets.Scripts.GUI;
using JetBrains.Annotations;
using UnityEngine;

public class PongMatchMenuUiController : BaseUiController
{
    [UsedImplicitly]
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ResumeMatch()
    {
        ServiceLocator.Get<GuiManager>().Close(this);
        Time.timeScale = 1;
    }

    [UsedImplicitly]
    public void QuitMatch()
    {
        ResumeMatch();
        ServiceLocator.Get<FsmManager>().GoToState<MainMenuState>();
    }

    [UsedImplicitly]
    public void RestartMatch()
    {
        ResumeMatch();
        ServiceLocator.Get<FsmManager>().GoToState<PongState>();
    }
}
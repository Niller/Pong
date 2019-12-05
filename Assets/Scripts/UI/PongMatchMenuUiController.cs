using Assets.Scripts.Fsm;
using UnityEngine;

public class PongMatchMenuUiController : BaseUiController
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ResumeMatch()
    {
        ServiceLocator.Get<GuiManager>().Close(this);
        Time.timeScale = 1;
    }

    public void QuitMatch()
    {
        ResumeMatch();
        ServiceLocator.Get<FsmManager>().GoToState<GuiState>();
    }

    public void RestartMatch()
    {
        ResumeMatch();
        ServiceLocator.Get<FsmManager>().GoToState<PongMatchState>();
    }
}
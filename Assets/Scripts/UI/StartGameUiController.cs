using Assets.Scripts.Fsm;
using UnityEngine;

public class StartGameUiController : MonoBehaviour
{
    public void StartGame()
    {
        ServiceLocator.Get<FsmManager>().GoToState<PongMatchState>();
    }
}
using System.Linq;
using Assets.Scripts.Fsm;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUiController : BaseUiController
{
    [SerializeField]
#pragma warning disable 649
    private ToggleGroup _difficultToggleGroup;
#pragma warning restore 649

    public void StartGame()
    {
        var fsmManager = ServiceLocator.Get<FsmManager>();
        
        // ReSharper disable once PossibleNullReferenceException
        fsmManager.SetBlackboardValue("Difficult", _difficultToggleGroup.ActiveToggles().First().transform.GetSiblingIndex());
        fsmManager.SetBlackboardValue("Multiplayer", false);

        fsmManager.GoToState<PongMatchState>();
    }
}
using System.Linq;
using Assets.Scripts.Framework.Fsm;
using Assets.Scripts.GameStates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUiController : BaseUiController
{
    [SerializeField]
#pragma warning disable 649
    private ToggleGroup _difficultToggleGroup;
#pragma warning restore 649

    [UsedImplicitly]
    public void StartGame()
    {
        var fsmManager = ServiceLocator.Get<FsmManager>();
        
        // ReSharper disable once PossibleNullReferenceException
        fsmManager.SetBlackboardValue("Difficult", _difficultToggleGroup.ActiveToggles().First().transform.GetSiblingIndex());

        fsmManager.GoToState<PongState>();
    }
}
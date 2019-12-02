using Assets.Scripts.Fsm;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public ViewConfig ViewConfig;

    private void Awake()
    {
        var fsmManager = ServiceLocator.Register(new FsmManager());
        ServiceLocator.Register<IInputSystem>(new KeyboardInputSystem());

        gameObject.AddComponent<GameManager>();

        var viewManager = gameObject.AddComponent<ViewManager>();
        viewManager.Initialize(ViewConfig);

        fsmManager.AddState<PongMatchState>();
        fsmManager.GoToState<PongMatchState>();

        Destroy(this);
    }
}
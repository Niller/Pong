using Assets.Scripts.Fsm;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    public ViewConfig ViewConfig;

    private void Awake()
    {
        var fsmManager = ServiceLocator.Register(new FsmManager());
        ServiceLocator.Register<IInputSystem>(new KeyboardInputSystem());
        ServiceLocator.Register(new PongManager());

        gameObject.AddComponent<ApplicationManager>();

        var viewManager = gameObject.AddComponent<ViewManager>();
        viewManager.Initialize(ViewConfig);

        fsmManager.AddState<PongMatchState>();
        fsmManager.GoToState<PongMatchState>();

        Destroy(this);
    }
}
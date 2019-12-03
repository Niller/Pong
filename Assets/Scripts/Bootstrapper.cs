using Assets.Scripts.Fsm;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private ViewConfig _viewConfig;
    [SerializeField]
    private GameConfig _gameConfig;

    private void Awake()
    {
        var fsmManager = ServiceLocator.Register(new FsmManager());
        ServiceLocator.Register<IInputSystem>(new KeyboardInputSystem());
        ServiceLocator.Register(new PongManager());
        ServiceLocator.Register(new SettingsManager()).Initialize(_gameConfig);

        gameObject.AddComponent<ApplicationManager>();

        var viewManager = gameObject.AddComponent<ViewManager>();
        viewManager.Initialize(_viewConfig);

        fsmManager.AddState<PongMatchState>();
        //fsmManager.GoToState<PongMatchState>();

        Destroy(this);
    }
}
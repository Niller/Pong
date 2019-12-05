using Assets.Scripts.Fsm;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private ViewConfig _viewConfig;

    [SerializeField]
    private GameConfig _gameConfig;

    [SerializeField]
    private GuiConfig _guiConfig;

    [SerializeField]
    private Transform _guiRoot;
#pragma warning restore 649

    private void Awake()
    {
        var fsmManager = ServiceLocator.Register(new FsmManager());

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        ServiceLocator.Register<IInputSystem>(new MobileInputSystem());
#else
        ServiceLocator.Register<IInputSystem>(new KeyboardInputSystem());
#endif

        ServiceLocator.Register(new GuiManager()).Initialize(_guiConfig, _guiRoot);
        ServiceLocator.Register(new PongManager());
        ServiceLocator.Register(new SettingsManager()).Initialize(_gameConfig);

        gameObject.AddComponent<ApplicationManager>();

        var viewManager = gameObject.AddComponent<ViewManager>();
        viewManager.Initialize(_viewConfig);

        fsmManager.AddState<GuiState>();
        fsmManager.AddState<PongMatchState>();
        
        fsmManager.GoToState<GuiState>();

        Destroy(this);
    }
}
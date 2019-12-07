using Assets.Scripts.Framework.Fsm;
using Assets.Scripts.GameStates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class StartMultiplayerGameUiController : BaseUiController
{
    [SerializeField]
    private Text _statusText;
    [SerializeField]
    private GameObject _buttons;

    private NetworkConnectionManager _networkConnectionManager;

    private void OnEnable()
    {
        _networkConnectionManager = ServiceLocator.Get<NetworkConnectionManager>();

        _networkConnectionManager.Connect();
        _buttons.SetActive(false);
        _statusText.text = "Connecting to Master...";
        _statusText.gameObject.SetActive(true);
        _networkConnectionManager.ConnectedToMaster += OnConnectedToMaster;
    }

    private void OnDisable()
    {
        _networkConnectionManager.ConnectedToMaster -= OnConnectedToMaster;
        _networkConnectionManager.GameReady -= OnGameReady;
    }

    private void OnConnectedToMaster()
    {
        _networkConnectionManager.ConnectedToMaster -= OnConnectedToMaster;
        _statusText.gameObject.SetActive(false);
        _buttons.SetActive(true);
    }

    private void OnGameReady()
    {
        var fsmManager = ServiceLocator.Get<FsmManager>();
        fsmManager.SetBlackboardValue("Difficult", 1);
        fsmManager.GoToState<PongMultiplayerState>();
    }

    [UsedImplicitly]
    public void Host()
    {
        _buttons.SetActive(false);
        _statusText.gameObject.SetActive(true);
        _networkConnectionManager.Host();
        _statusText.text = "Wait player...";
        _networkConnectionManager.GameReady += OnGameReady;
    }

    [UsedImplicitly]
    public void Join()
    {
        _buttons.SetActive(false);
        _statusText.gameObject.SetActive(true);
        _networkConnectionManager.Join();
        _statusText.text = "Finding room...";
        _networkConnectionManager.GameReady += OnGameReady;
    }

    [UsedImplicitly]
    public override void Back()
    {
        ServiceLocator.Get<NetworkConnectionManager>().Disconnect();
    }
}
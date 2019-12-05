using Assets.Scripts.Fsm;
using UnityEngine;
using UnityEngine.UI;

public class StartMultiplayerGameUiController : BaseUiController
{
    [SerializeField]
    private Text _statusText;
    [SerializeField]
    private GameObject _buttons;

    private NetworkManager _networkManager;

    private void OnEnable()
    {
        if (!ServiceLocator.TryGet<NetworkManager>(out _networkManager))
        {
            _networkManager = CreateNetworkManager();
        }

        _networkManager.Connect();
        _buttons.SetActive(false);
        _statusText.text = "Connecting to Master...";
        _statusText.gameObject.SetActive(true);
        _networkManager.ConnectedToMaster += OnConnectedToMaster;
    }

    private void OnDisable()
    {
        _networkManager.ConnectedToMaster -= OnConnectedToMaster;
        _networkManager.GameReady -= OnGameReady;
    }

    private void OnConnectedToMaster()
    {
        _networkManager.ConnectedToMaster -= OnConnectedToMaster;
        _statusText.gameObject.SetActive(false);
        _buttons.SetActive(true);
    }

    private NetworkManager CreateNetworkManager()
    {
        var networkManagerGo = new GameObject("Network manager");
        var networkManager = networkManagerGo.AddComponent<NetworkManager>();
        return ServiceLocator.Register(networkManager);
    }

    private void OnGameReady()
    {
        ServiceLocator.Get<FsmManager>().GoToState<PongMatchState>();
    }

    public void Host()
    {
        _buttons.SetActive(false);
        _statusText.gameObject.SetActive(true);
        _networkManager.Host();
        _statusText.text = "Wait player...";
        _networkManager.GameReady += OnGameReady;
    }

    public void Join()
    {
        _buttons.SetActive(false);
        _statusText.gameObject.SetActive(true);
        _networkManager.Join();
        _statusText.text = "Finding room...";
        _networkManager.GameReady += OnGameReady;
    }
}
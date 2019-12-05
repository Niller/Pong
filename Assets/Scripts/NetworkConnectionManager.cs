using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{
    private GameObject _networkingGameObject;
    private GameObject _networkingInstance;

    public bool ConnectingToMaster
    {
        get;
        private set;
    }

    public event Action ConnectedToMaster;
    public event Action GameReady;
    public event Action Disconnected;

    public void Initialize(GameObject networkingGameGameObject)
    {
        _networkingGameObject = networkingGameGameObject;
    }

    public void Connect()
    {
        ConnectingToMaster = true;

        PhotonNetwork.OfflineMode = false; 
        PhotonNetwork.NickName = "PlayerName" + Random.Range(0, int.MaxValue);
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.SendRate = 30;

        if (!PhotonNetwork.OfflineMode)
            PhotonNetwork.ConnectUsingSettings();   
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        ConnectingToMaster = false;
        Debug.Log("Connected to Master!");
        ConnectedToMaster?.Invoke();
    }

    public void Host()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public void Join()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("OnJoinedRoom " + 
                  "Master: " + PhotonNetwork.IsMasterClient + 
                  " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + 
                  " | RoomName: " + PhotonNetwork.CurrentRoom.Name + 
                  " Region: " + PhotonNetwork.CloudRegion);
        TryStartGame();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        ConnectingToMaster = false;
        PhotonNetwork.Destroy(_networkingInstance);
        _networkingInstance = null;
        Debug.Log(cause);
        Disconnected?.Invoke();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("OnPlayerEnteredRoom " +
                  "Master: " + PhotonNetwork.IsMasterClient + 
                  " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + 
                  " | RoomName: " + PhotonNetwork.CurrentRoom.Name + 
                  " Region: " + PhotonNetwork.CloudRegion);
        TryStartGame();
    }

    private void TryStartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            _networkingInstance = PhotonNetwork.Instantiate(_networkingGameObject.name, Vector3.zero, Quaternion.identity);
            ServiceLocator.Register(_networkingInstance.AddComponent<NetworkGameManager>());
            GameReady?.Invoke();
        }
    }

}
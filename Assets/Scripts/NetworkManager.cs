using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public bool ConnectingToMaster
    {
        get;
        private set;
    }

    public event Action ConnectedToMaster;
    public event Action GameReady;
    public event Action Disconnected;

    public void Connect()
    {
        ConnectingToMaster = true;

        //Settings (all optional and only for tutorial purpose)
        PhotonNetwork.OfflineMode = false;           //true would "fake" an online connection
        PhotonNetwork.NickName = "PlayerName";       //to set a player name
        PhotonNetwork.AutomaticallySyncScene = true; //to call PhotonNetwork.LoadLevel()
        PhotonNetwork.GameVersion = "v1";            //only people with the same game version can play together

        //PhotonNetwork.ConnectToMaster(ip,port,appid); //manual connection
        if (!PhotonNetwork.OfflineMode)
            PhotonNetwork.ConnectUsingSettings();           //automatic connection based on the config file in Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset
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
            GameReady?.Invoke();
        }
    }

}
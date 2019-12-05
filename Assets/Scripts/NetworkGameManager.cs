using Assets.Scripts;
using Assets.Scripts.Signals;
using Photon.Pun;
using UnityEngine;

public class NetworkGameManager : MonoBehaviour
{
    [PunRPC]
    private void PaddleInput(int direction, float force)
    {
        SignalBus.Invoke(new MoveInputSignal(1, direction, force));
    }

    public void CallPaddleInput(int direction, float force)
    {
        PhotonView.Get(this).RPC("PaddleInput", RpcTarget.Others, direction, force);
        
    }
}
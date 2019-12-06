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

    [PunRPC]
    private void SyncBall(Vector2 position, Vector2 direction, float speed)
    {
        ServiceLocator.Get<PongManager>().SyncBall(position, direction, speed);
    }

    [PunRPC]
    private void SyncPaddle(float position)
    {
        ServiceLocator.Get<PongManager>().Paddle1.Sync(position);
    }

    [PunRPC]
    private void SyncScore(int score)
    {
        ServiceLocator.Get<PongManager>().SyncScore(score);
    }


    public void CallPaddleInput(int direction, float force)
    {
        PhotonView.Get(this).RPC("PaddleInput", RpcTarget.Others, direction, force);
        
    }

    [PunRPC]
    public void CallSyncBall(Ball ball)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView.Get(this).RPC("SyncBall", RpcTarget.Others, ball.Position * new Vector2(1, -1), ball.Direction, ball.Speed);
        }
    }

    [PunRPC]
    public void CallSyncScore(int score)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView.Get(this).RPC("SyncScore", RpcTarget.Others, score);
        }
    }

    [PunRPC]
    public void CallSyncPaddle(Paddle paddle)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView.Get(this).RPC("SyncPaddle", RpcTarget.Others, paddle.Position.x);
        }
    }
}
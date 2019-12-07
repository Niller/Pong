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
        ((PongMultiplayerManager)ServiceLocator.Get<PongManager>()).SyncBall(position, direction, speed);
    }

    [PunRPC]
    private void SyncPaddle(float position)
    {
        ((PaddleMultiplayer)ServiceLocator.Get<PongManager>().Paddle1).Sync(position);
    }

    [PunRPC]
    private void SyncScore(int score)
    {
        ((PongMultiplayerManager)ServiceLocator.Get<PongManager>()).SyncScore(score);
    }

    [PunRPC]
    private void PaddleHitBall(int index, float point)
    {
        SignalBus.Invoke(new BallHitSignal(index, point));
    }


    public void CallPaddleInput(int direction, float force)
    {
        PhotonView.Get(this).RPC("PaddleInput", RpcTarget.Others, direction, force);
        
    }

    public void CallSyncBall(Ball ball)
    {
        PhotonView.Get(this).RPC("SyncBall", RpcTarget.Others, ball.Position * new Vector2(1, -1), ball.Direction, ball.Speed);
    }

    public void CallSyncScore(int score)
    {
        PhotonView.Get(this).RPC("SyncScore", RpcTarget.Others, score);
    }

    public void CallSyncPaddle(Paddle paddle)
    {
        PhotonView.Get(this).RPC("SyncPaddle", RpcTarget.Others, paddle.Position.x);
    }

    public void CallPaddleHitBall(int index, float point)
    {
        PhotonView.Get(this).RPC("PaddleHitBall", RpcTarget.Others, index == 0 ? 1 : 0, point);
    }
}
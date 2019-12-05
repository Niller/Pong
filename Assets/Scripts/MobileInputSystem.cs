using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class MobileInputSystem : IInputSystem
{
    public void Update(float deltaTime)
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        var delta = Input.GetTouch(0).deltaPosition;
        var direction = (int) Mathf.Sign(delta.x);
        var force = Mathf.Abs(delta.x / Screen.width);
        SignalBus.Invoke(new MoveInputSignal(0, direction, force));
        if (!ServiceLocator.Get<PongManager>().IsMultiplayer)
        {
            SignalBus.Invoke(new MoveInputSignal(1, direction, force));
        }
        else
        {
            ServiceLocator.Get<NetworkGameManager>().CallPaddleInput(direction, force);
        }
    }
}
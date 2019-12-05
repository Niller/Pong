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

        SignalBus.Invoke(new MoveInputSignal((int)Mathf.Sign(delta.x), Mathf.Abs(delta.x / Screen.width)));
    }
}
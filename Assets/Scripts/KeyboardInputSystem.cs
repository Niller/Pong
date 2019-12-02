using Assets.Scripts;
using Assets.Scripts.Signals;
using UnityEngine;

public class KeyboardInputSystem : IInputSystem
{
    private const float DefaultForce = 0.01f;

    public void Update(float deltaTime)
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SignalBus.Invoke(new MoveInputSignal(1, DefaultForce));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SignalBus.Invoke(new MoveInputSignal(-1, DefaultForce));
        }
    }
}
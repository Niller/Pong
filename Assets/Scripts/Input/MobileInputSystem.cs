using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class MobileInputSystem : IInputSystem
    {
        private const float Speed = 70f;

        public void Update(float deltaTime)
        {
            if (UnityEngine.Input.touchCount <= 0)
            {
                return;
            }

            var delta = UnityEngine.Input.GetTouch(0).deltaPosition;
            var direction = (int) Mathf.Sign(delta.x);
            var force = Mathf.Abs(delta.x / Screen.width) * Speed * deltaTime;
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
}
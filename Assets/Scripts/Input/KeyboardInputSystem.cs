using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class KeyboardInputSystem : IInputSystem
    {
        private const float DefaultForce = 0.6f;

        public void Update(float deltaTime)
        {
            var force = DefaultForce * deltaTime;
            if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                SignalBus.Invoke(new MoveInputSignal(0, 1, force));
                if (!ServiceLocator.Get<PongManager>().IsMultiplayer)
                {
                    SignalBus.Invoke(new MoveInputSignal(1, 1, force));
                }
                else
                {
                    ServiceLocator.Get<NetworkGameManager>().CallPaddleInput(1, force);
                }
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                SignalBus.Invoke(new MoveInputSignal(0, -1, force));
                if (!ServiceLocator.Get<PongManager>().IsMultiplayer)
                {
                    SignalBus.Invoke(new MoveInputSignal(1, -1, force));
                }
                else
                {
                    ServiceLocator.Get<NetworkGameManager>().CallPaddleInput(-1, force);
                }
            }
        
        }
    
    }
}
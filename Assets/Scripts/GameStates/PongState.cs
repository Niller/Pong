using Assets.Scripts.Framework.Fsm;
using Assets.Scripts.GUI;
using Assets.Scripts.Input;
using Assets.Scripts.Signals;

namespace Assets.Scripts.GameStates
{
    public class PongState : BaseState
    {
        private PongManager _pongManager;
        private IInputSystem _inputSystem;

        protected virtual PongManager CreatePongManager()
        {
            return ServiceLocator.Register(new PongManager());
        }

        public override void Enter()
        {
            _inputSystem = ServiceLocator.Get<IInputSystem>();
            ServiceLocator.Get<GuiManager>().Open(GuiViewType.Match, true);

            var difficult = (int) FsmManager.GetBlackboardValue("Difficult");

            _pongManager = CreatePongManager();
            _pongManager.Initialize(ServiceLocator.Get<SettingsManager>().Config.Difficulties[difficult]);
            _pongManager.SpawnPaddles();
            _pongManager.SpawnBall();

            SignalBus.Invoke(new GameStartedSignal(_pongManager.Paddle1, _pongManager.Paddle2));
        }

        public override void Exit()
        {
            _pongManager.Dispose();
            SignalBus.Invoke(new MatchStopSignal());
            ServiceLocator.Get<GuiManager>().CloseAll();
        }

        public override void Execute(float deltaTime)
        {
            _inputSystem.Update(deltaTime);
            _pongManager.Update(deltaTime);
        }
    }
}
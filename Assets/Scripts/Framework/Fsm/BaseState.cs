namespace Assets.Scripts.Framework.Fsm
{
    public abstract class BaseState : IState
    {
        protected FsmManager FsmManager;

        public void Initialize(FsmManager manager)
        {
            FsmManager = manager;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Execute(float deltaTime);
    }
}
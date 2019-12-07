namespace Assets.Scripts.Framework.Fsm
{
    public interface IState
    {
        void Initialize(FsmManager manager);
        void Enter();
        void Exit();
        void Execute(float deltaTime);
    }
}
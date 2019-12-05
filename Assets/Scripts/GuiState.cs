using Assets.Scripts.Fsm;

public class GuiState : BaseState
{
    public override void Enter()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.MainBackground, true);
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.Main);
    }

    public override void Exit()
    {
        ServiceLocator.Get<GuiManager>().CloseAll();
    }

    public override void Execute(float deltaTime)
    {
    }
}
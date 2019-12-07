using Assets.Scripts.Fsm;

public class MainMenuState : BaseState
{
    public override void Enter()
    {
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.MainMenuFullscreen, true);
        ServiceLocator.Get<GuiManager>().Open(GuiViewType.MainMenu);
    }

    public override void Exit()
    {
        ServiceLocator.Get<GuiManager>().CloseAll();
    }

    public override void Execute(float deltaTime)
    {
    }
}
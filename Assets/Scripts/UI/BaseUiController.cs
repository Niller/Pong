using UnityEngine;

public abstract class BaseUiController : MonoBehaviour, IGuiController
{
    public GameObject Root => gameObject;

    public void Back()
    {
        ServiceLocator.Get<GuiManager>().Back();
    }
}
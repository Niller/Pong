using UnityEngine;

public abstract class BaseUiController : MonoBehaviour, IGuiController
{
    public GameObject Root => gameObject;

    public virtual void Back()
    {
        ServiceLocator.Get<GuiManager>().Back();
    }
}
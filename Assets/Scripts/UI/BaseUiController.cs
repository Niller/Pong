using Assets.Scripts.GUI;
using JetBrains.Annotations;
using UnityEngine;

public abstract class BaseUiController : MonoBehaviour, IGuiController
{
    public GameObject Root => gameObject;

    [UsedImplicitly]
    public virtual void Back()
    {
        ServiceLocator.Get<GuiManager>().Back();
    }
}
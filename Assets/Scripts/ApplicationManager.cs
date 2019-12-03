using Assets.Scripts.Fsm;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    private FsmManager _fsmManager;
    private IInputSystem _inputSystem;

    private void Awake()
    {
        _fsmManager = ServiceLocator.Get<FsmManager>();
        _inputSystem = ServiceLocator.Get<IInputSystem>();
    }

    private void Update()
    {
        _fsmManager.Execute(Time.deltaTime);
        _inputSystem.Update(Time.deltaTime);
    }
}
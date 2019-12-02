using Assets.Scripts.Fsm;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FsmManager _fsmManager;

    private void Awake()
    {
        _fsmManager = ServiceLocator.Get<FsmManager>();
    }

    private void Update()
    {
        _fsmManager.Execute(Time.deltaTime);
    }
}
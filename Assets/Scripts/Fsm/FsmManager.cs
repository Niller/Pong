using System;
using System.Collections.Generic;

namespace Assets.Scripts.Fsm
{
    public class FsmManager : IDisposable
    {
        private readonly Dictionary<string, object> _blackboard = new Dictionary<string, object>();

        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;

        public FsmManager()
        {

        }

        public void AddState<T>() where T : IState, new()
        {
            var state = new T();
            state.Initialize(this);
            _states.Add(typeof(T), state);
        }

        public void GoToState<T>()
        {
            _currentState?.Exit();
            if (_states.TryGetValue(typeof(T), out var state))
            {
                _currentState = state;
                _currentState.Enter();
            }
        }

        public object GetBlackboardValue(string name)
        {
            _blackboard.TryGetValue(name, out var result);
            return result;
        }

        public void SetBlackboardValue(string name, object value)
        {
            _blackboard[name] = value;
        }

        public void Execute(float deltaTime)
        {
            _currentState?.Execute(deltaTime);
        }

        public void Dispose()
        {
            _currentState?.Exit();

            _blackboard.Clear();
            _states.Clear();
            _currentState = null;
        }
    }
}

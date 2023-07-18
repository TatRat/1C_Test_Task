using System;
using System.Collections.Generic;
using StateMachine.CodeBase.Units.Units;

namespace StateMachine
{
    public abstract class BaseStateMachine<TState> where TState : IState
    {
        private readonly Dictionary<Type, TState> _states = new Dictionary<Type, TState>();
        private TState _previousState;
        private TState _currentState;
        private bool _enabled = true;

        public event Action<TState> StateChanged;

        public TState PreviousState => _previousState;
        public TState CurrentState => _currentState;

        public void Enable() =>
            _enabled = true;

        public void Disable()
        {
            _enabled = false;
            _currentState?.Exit();
            _previousState = _currentState;
        }

        public void AddState<T>(T state) where T : TState=>
            _states.Add(typeof(T), state);

        public void ChangeState<T>() where T : TState
        {
            _currentState?.Exit();
            _previousState = _currentState;
            _currentState = _states[typeof(T)];
            _currentState.Enter();

            StateChanged?.Invoke(_currentState);
        }

        public void Update()
        {
            if (!_enabled)
                return;

            _currentState.Update();
        }

        public void FixedUpdate()
        {
            if (!_enabled)
                return;

            _currentState.FixedUpdate();
        }
    }
}
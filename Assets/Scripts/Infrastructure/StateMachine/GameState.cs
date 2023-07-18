using StateMachine.CodeBase.Units.Units;

namespace Infrastructure.StateMachine
{
    public abstract class GameState : IState
    {
        protected GameStateMachine _gameStateMachine;
        protected EventProvider.EventProvider _eventProvider;

        public GameState(GameStateMachine gameStateMachine, EventProvider.EventProvider eventProvider)
        {
            _eventProvider = eventProvider;
            _gameStateMachine = gameStateMachine;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Destruct()
        {
        }
    }
}
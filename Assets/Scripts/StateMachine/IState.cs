namespace StateMachine
{
    namespace CodeBase.Units.Units
    {
        public interface IState
        {
            public void Enter();
            public void Update();
            public void FixedUpdate();
            public void Exit();
            public void Destruct();
        }
    }
}
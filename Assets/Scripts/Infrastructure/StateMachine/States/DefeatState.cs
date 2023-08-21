using EventProvider.Events;
using Factories;
using HUD;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class DefeatState : GameState
    {
        private HudFactory _hudFactory;
        private EndGameHeadsUpDisplay _endGameHeadsUpDisplay;

        public DefeatState(GameStateMachine gameStateMachine, EventProvider.EventProvider eventProvider,
            HudFactory hudFactory) : base(gameStateMachine, eventProvider) =>
            _hudFactory = hudFactory;

        public override async void Enter()
        {
            base.Enter();
            _endGameHeadsUpDisplay = await _hudFactory.GetDefeatHeadsUpDisplay();
            _endGameHeadsUpDisplay.Enable();
            _eventProvider.Subscribe<RestartButtonPressedEvent>(OnRestartButtonPressed);
        }

        public override void Exit()
        {
            base.Exit();
            _eventProvider.UnSubscribe<RestartButtonPressedEvent>(OnRestartButtonPressed);
            _endGameHeadsUpDisplay.Disable();
            Object.Destroy(_endGameHeadsUpDisplay.gameObject);
        }

        private void OnRestartButtonPressed(RestartButtonPressedEvent restartButtonPressedEvent) => 
            _gameStateMachine.ChangeState<RoundState>();
    }
}
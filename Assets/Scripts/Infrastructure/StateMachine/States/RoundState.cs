using Configs;
using EventProvider.Events;
using Factories;
using GameLogic.GameZones;
using GameLogic.Player;
using HUD;
using Input;
using UnityEditorInternal.VersionControl;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class RoundState : GameState
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly GameFactory _gameFactory;
        private readonly HudFactory _hudFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly Transform _areasContainer;
        private readonly Transform _enemiesContainer;
        private readonly Camera _camera;
        private readonly GameSettingsConfig _gameSettingsConfig;
        private readonly EnemyConfig _enemyConfig;
        private readonly PlayerConfig _playerConfig;
        private readonly IInputService _inputService;

        private PlayerAreaView _playerAreaView;
        private EnemySpawnerView _enemySpawnerView;
        private int _health;
        private int _enemiesToKill;
        private PlayerUnit _playerUnit;
        private RoundHeadsUpDisplay _roundHeadsUpDisplay;

        public RoundState(GameStateMachine gameStateMachine,
            EventProvider.EventProvider eventProvider,
            EnemyFactory enemyFactory,
            GameFactory gameFactory,
            HudFactory hudFactory,
            PlayerFactory playerFactory,
            Transform areasContainer,
            Transform enemiesContainer,
            Camera camera,
            GameSettingsConfig gameSettingsConfig,
            EnemyConfig enemyConfig,
            PlayerConfig playerConfig, IInputService inputService) : base(gameStateMachine,
            eventProvider)
        {
            _inputService = inputService;
            _enemyFactory = enemyFactory;
            _gameFactory = gameFactory;
            _hudFactory = hudFactory;
            _playerFactory = playerFactory;
            _areasContainer = areasContainer;
            _enemiesContainer = enemiesContainer;
            _camera = camera;
            _gameSettingsConfig = gameSettingsConfig;
            _enemyConfig = enemyConfig;
            _playerConfig = playerConfig;
        }

        public async override void Enter()
        {
            _health = _playerConfig.PlayerStartHealth;
            _enemiesToKill = Random.Range(_enemyConfig.MinEnemiesCount, _enemyConfig.MaxEnemiesCount);

            _roundHeadsUpDisplay = await _hudFactory.GetRoundHeadsUpDisplay(_eventProvider, _health);
            
            Vector2 playerAreaRightTopPoint = _camera.ViewportToWorldPoint(new Vector3(1,
                _gameSettingsConfig.FinishLineVerticalPositionOnScreenPercent, _camera.nearClipPlane));
            Vector2 playerAreaLeftDownPoint = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
            
            _playerAreaView = await _gameFactory.GetPlayerAreaView(playerAreaRightTopPoint, playerAreaLeftDownPoint, _areasContainer);
            _enemySpawnerView = await _gameFactory.GetEnemySpawnerView(_enemyFactory, _enemyConfig, _enemiesContainer,
                Random.Range(_enemyConfig.MinEnemySpawnTimeOut, _enemyConfig.MaxEnemySpawnTimeOut), _camera.ViewportToWorldPoint(new Vector3(0.5f,
                    _gameSettingsConfig.EnemySpawnersVerticalPositionOnScreenPercent, _camera.nearClipPlane)), _areasContainer);
            _playerUnit = await _playerFactory.GetPlayerUnit(_inputService, new PlayerModel(_playerConfig.PlayerSpeed, _playerConfig.AttackRate, _playerConfig.AutomaticAttackRange, playerAreaRightTopPoint, playerAreaLeftDownPoint), _playerFactory, _camera.ViewportToWorldPoint(new Vector3(0.5f,
                _gameSettingsConfig.FinishLineVerticalPositionOnScreenPercent / 2, _camera.nearClipPlane)));

            _enemySpawnerView.EnemyKilled += OnEnemyKilled;
            _playerAreaView.EnemyReachedFinish += OnEnemyReachedDestination;
            
            _playerUnit.Enable();
            _inputService.Enable();
            _roundHeadsUpDisplay.Enable();
        }

        private void OnEnemyReachedDestination()
        {
            _health--;
            _eventProvider.Invoke(new EnemyReachedFinishLineEvent(_health));
            
            if (_health > 0)
                return;
            
            _gameStateMachine.ChangeState<DefeatState>();
        }

        private void OnEnemyKilled()
        {
            _enemiesToKill--;
            if(_enemiesToKill > 0)
                return;
            
            _gameStateMachine.ChangeState<WinState>();
        }

        public override void Exit()
        {
            _enemySpawnerView.EnemyKilled -= OnEnemyKilled;
            _playerAreaView.EnemyReachedFinish -= OnEnemyReachedDestination;
            
            _playerUnit.Disable();
            _playerAreaView.Destruct();
            _enemySpawnerView.Destruct();
            _inputService.Disable();
            _roundHeadsUpDisplay.Disable();
            
            Object.Destroy(_playerUnit.gameObject);
            Object.Destroy(_playerAreaView.gameObject);
            Object.Destroy(_enemySpawnerView.gameObject);
            Object.Destroy(_roundHeadsUpDisplay.gameObject);
            
        }
    }
}
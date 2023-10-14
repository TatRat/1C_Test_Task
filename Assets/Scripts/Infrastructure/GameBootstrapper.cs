using System.Collections;
using System.Threading.Tasks;
using Configs;
using Cysharp.Threading.Tasks;
using Factories;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        [Header("Containers")]
        [SerializeField] private Transform areasContainer;
        [SerializeField] private Transform enemiesContainer;
        [SerializeField] private Transform playerContainer;
        [SerializeField] private Transform hudContainer;

        private GameStateMachine _gameStateMachine;

        private GameSettingsConfig _gameSettingsConfig;
        private EnemyConfig _enemyConfig;
        private PlayerConfig _playerConfig;
        private PlayerFactory _playerFactory;
        private GameFactory _gameFactory;
        private HudFactory _hudFactory;
        private EventProvider.EventProvider _eventProvider;
        private IInputService _inputService;

        [Inject]
        private void Construct(HudFactory hudFactory, GameFactory gameFactory, PlayerFactory playerFactory, EventProvider.EventProvider eventProvider, IInputService inputService)
        {
            _inputService = inputService;
            _eventProvider = eventProvider;
            _hudFactory = hudFactory;
            _gameFactory = gameFactory;
            _playerFactory = playerFactory;
        }

        private async void Start() => 
            await Bootstrap();

        private async UniTask Bootstrap()
        {
            await BindFactories();
            await BindConfigs();
            await BindStateMachine();
            await PostBind();
        } 
        
        private async UniTask BindStateMachine()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.AddState(new RoundState(_gameStateMachine, _eventProvider, _gameFactory,
                _hudFactory, _playerFactory, areasContainer, enemiesContainer, camera, _gameSettingsConfig,
                _enemyConfig, _playerConfig, _inputService));
            _gameStateMachine.AddState(new WinState(_gameStateMachine, _eventProvider, _hudFactory));
            _gameStateMachine.AddState(new DefeatState(_gameStateMachine, _eventProvider, _hudFactory));
        }

        private async UniTask BindFactories()
        {
            _hudFactory.Initialize(hudContainer);
            await _playerFactory.Initialize(playerContainer);
        }
        
        private async UniTask BindConfigs() => 
            await LoadConfigs();

        private async UniTask PostBind()
        {
            _gameStateMachine.ChangeState<RoundState>();
            _gameStateMachine.Enable();
        }

        private async UniTask LoadConfigs()
        {
            _gameSettingsConfig = await _gameFactory.GetConfig<GameSettingsConfig>();
            _enemyConfig = await _gameFactory.GetConfig<EnemyConfig>();
            _playerConfig = await _gameFactory.GetConfig<PlayerConfig>();
        }
    }
}
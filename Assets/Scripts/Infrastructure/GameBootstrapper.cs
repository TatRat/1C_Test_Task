using System.Collections;
using Configs;
using Factories;
using Infrastructure.Services;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Input;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private MonoService monoService;
        [SerializeField] private Camera camera;

        [Header("Containers")]
        [SerializeField] private Transform areasContainer;
        [SerializeField] private Transform enemiesContainer;
        [SerializeField] private Transform playerContainer;
        [SerializeField] private Transform hudContainer;

        private GameStateMachine _gameStateMachine;
        private EventProvider.EventProvider _eventProvider;
        private IInputService _inputService;
        
        private HudFactory _hudFactory;
        private EnemyFactory _enemyFactory;
        private GameFactory _gameFactory;
        private PlayerFactory _playerFactory;
        
        private GameSettingsConfig _gameSettingsConfig;
        private EnemyConfig _enemyConfig;
        private PlayerConfig _playerConfig;

        private void Start() => 
            monoService.StartCoroutine(BootstrapCoroutine());

        private IEnumerator BootstrapCoroutine()
        {
            yield return BindServices();
            yield return BindFactories();
            yield return BindConfigs();
            yield return BindStateMachine();
            yield return PostBind();
        }

        private IEnumerator BindServices()
        {
            _eventProvider = new EventProvider.EventProvider();
            _inputService = new DesktopInputService(monoService);
            
            yield break;
        }

        private IEnumerator BindStateMachine()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.AddState(new RoundState(_gameStateMachine, _eventProvider, _enemyFactory, _gameFactory,
                _hudFactory, _playerFactory, areasContainer, enemiesContainer, camera, _gameSettingsConfig,
                _enemyConfig, _playerConfig, _inputService));
            _gameStateMachine.AddState(new WinState(_gameStateMachine, _eventProvider, _hudFactory));
            _gameStateMachine.AddState(new DefeatState(_gameStateMachine, _eventProvider, _hudFactory));

            yield break;
        }

        private IEnumerator BindFactories()
        {
            _hudFactory = new HudFactory(hudContainer);
            _playerFactory = new PlayerFactory(playerContainer);
            _gameFactory = new GameFactory();
            _enemyFactory = new EnemyFactory();
            
            yield break;
        }
        
        private IEnumerator BindConfigs()
        {
            _gameSettingsConfig = _gameFactory.GetConfig<GameSettingsConfig>();
            _enemyConfig = _gameFactory.GetConfig<EnemyConfig>();
            _playerConfig = _gameFactory.GetConfig<PlayerConfig>();
            
            yield break;
        }

        private IEnumerator PostBind()
        {
            _gameStateMachine.ChangeState<RoundState>();
            _gameStateMachine.Enable();
            
            yield break;
        }
    }
}
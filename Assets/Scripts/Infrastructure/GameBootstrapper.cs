using System.Collections;
using System.Threading.Tasks;
using Configs;
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
        private MonoService _monoService;
        private PlayerFactory _playerFactory;
        private GameFactory _gameFactory;
        private EnemyFactory _enemyFactory;
        private HudFactory _hudFactory;
        private EventProvider.EventProvider _eventProvider;
        private IInputService _inputService;

        [Inject]
        private void Construct(MonoService monoService, HudFactory hudFactory, EnemyFactory enemyFactory, GameFactory gameFactory, PlayerFactory playerFactory, EventProvider.EventProvider eventProvider, IInputService inputService)
        {
            _inputService = inputService;
            _eventProvider = eventProvider;
            _hudFactory = hudFactory;
            _enemyFactory = enemyFactory;
            _gameFactory = gameFactory;
            _playerFactory = playerFactory;
            _monoService = monoService;
        }

        private void Start() => 
            _monoService.StartCoroutine(BootstrapCoroutine());

        private IEnumerator BootstrapCoroutine()
        {
            yield return BindFactories();
            yield return BindConfigs();
            yield return BindStateMachine();
            yield return PostBind();
        } 
        
        private IEnumerator BindStateMachine()
        {
            _gameStateMachine = new GameStateMachine();
            _gameStateMachine.AddState(new RoundState(_gameStateMachine, _eventProvider, _gameFactory,
                _hudFactory, _playerFactory, areasContainer, enemiesContainer, camera, _gameSettingsConfig,
                _enemyConfig, _playerConfig, _inputService));
            _gameStateMachine.AddState(new WinState(_gameStateMachine, _eventProvider, _hudFactory));
            _gameStateMachine.AddState(new DefeatState(_gameStateMachine, _eventProvider, _hudFactory));

            yield break;
        }

        private IEnumerator BindFactories()
        {
            _hudFactory.Initialize(hudContainer);
            _playerFactory.Initialize(playerContainer);

            yield break;
        }
        
        private IEnumerator BindConfigs()
        {
            Task loadConfigs = LoadConfigs();
            yield return new WaitUntil(() => loadConfigs.IsCompleted);
        }

        private IEnumerator PostBind()
        {
            _gameStateMachine.ChangeState<RoundState>();
            _gameStateMachine.Enable();
            
            yield break;
        }

        private async Task LoadConfigs()
        {
            _gameSettingsConfig = await _gameFactory.GetConfig<GameSettingsConfig>();
            _enemyConfig = await _gameFactory.GetConfig<EnemyConfig>();
            _playerConfig = await _gameFactory.GetConfig<PlayerConfig>();
        }
    }
}
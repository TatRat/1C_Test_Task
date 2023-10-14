using System.Threading.Tasks;
using Configs;
using Cysharp.Threading.Tasks;
using GameLogic.GameZones;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class GameFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        
        private const string PlayerAreaViewAddress = "PlayerAreaView";
        private const string EnemySpawnerViewAddress = "EnemySpawnerView";

        public GameFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public async UniTask<T> GetConfig<T>() where T : ScriptableObject =>
            await _assetProvider.Load<T>($"{typeof(T).Name}");

        public async UniTask<PlayerAreaView> GetPlayerAreaView(Vector2 playerAreaRightTopPoint, Vector2 playerAreaLeftDownPoint, Transform parent)
        {
            PlayerAreaView playerAreaView = _diContainer.InstantiatePrefabForComponent<PlayerAreaView>(await _assetProvider.Load<GameObject>(PlayerAreaViewAddress), parent);
            playerAreaView.Initialize(playerAreaRightTopPoint, playerAreaLeftDownPoint);
            
            return playerAreaView;
        }

        public async UniTask<EnemySpawnerView> GetEnemySpawnerView(EnemyConfig enemyConfig, Transform enemiesContainer, Vector3 spawnPosition, Transform parent)
        {
            EnemySpawnerView enemySpawnerView = _diContainer.InstantiatePrefabForComponent<EnemySpawnerView>(await _assetProvider.Load<GameObject>(EnemySpawnerViewAddress), spawnPosition, Quaternion.identity, parent);
            enemySpawnerView.Initialize(enemyConfig, enemiesContainer,
                Random.Range(enemyConfig.MinEnemySpawnTimeOut, enemyConfig.MaxEnemySpawnTimeOut));
            
            return enemySpawnerView;
        }
    }
}
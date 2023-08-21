using Configs;
using GameLogic.GameZones;
using UnityEngine;

namespace Factories
{
    public class GameFactory
    {
        private const string ConfigsPath = "Configs/";
        private const string PlayerAreaViewPath = "Prefabs/GameZones/PlayerAreaView";
        private const string EnemySpawnerViewPath = "Prefabs/GameZones/EnemySpawnerView";

        public T GetConfig<T>() where T : ScriptableObject =>
            Resources.Load<T>($"{ConfigsPath}{typeof(T).Name}");

        public PlayerAreaView GetPlayerAreaView(Vector2 playerAreaRightTopPoint, Vector2 playerAreaLeftDownPoint, Transform parent)
        {
            PlayerAreaView playerAreaView = UnityEngine.Object.Instantiate(Resources.Load<PlayerAreaView>(PlayerAreaViewPath), parent);
            playerAreaView.Initialize(playerAreaRightTopPoint, playerAreaLeftDownPoint);
            
            return playerAreaView;
        }

        public EnemySpawnerView GetEnemySpawnerView(EnemyFactory enemyFactory, EnemyConfig enemyConfig, Transform enemiesContainer, float range, Vector3 spawnPosition, Transform parent)
        {
            EnemySpawnerView enemySpawnerView = UnityEngine.Object.Instantiate<EnemySpawnerView>(Resources.Load<EnemySpawnerView>(EnemySpawnerViewPath), spawnPosition, Quaternion.identity, parent);
            enemySpawnerView.Initialize(enemyFactory, enemyConfig, enemiesContainer,
                Random.Range(enemyConfig.MinEnemySpawnTimeOut, enemyConfig.MaxEnemySpawnTimeOut));
            
            return enemySpawnerView;
        }
    }
}
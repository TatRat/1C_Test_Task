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

        public PlayerAreaView GetPlayerAreaView(Transform parent) => 
            UnityEngine.Object.Instantiate(Resources.Load<PlayerAreaView>(PlayerAreaViewPath), parent);

        public EnemySpawnerView GetEnemySpawnerView(Vector3 spawnPosition, Transform parent) => 
            UnityEngine.Object.Instantiate(Resources.Load<EnemySpawnerView>(EnemySpawnerViewPath), spawnPosition, Quaternion.identity, parent);
    }
}
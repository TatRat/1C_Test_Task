using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Configs/EnemyConfig")]
    public class EnemyConfig  : ScriptableObject
    {
        [Header("Enemy units settings")] 
        [SerializeField] private float minEnemySpeed;
        [SerializeField] private float maxEnemySpeed;
        [SerializeField] private int enemyStartHealth;
        
        [Header("Enemies spawn settings")] 
        [SerializeField] private int minEnemiesCount;
        [SerializeField] private int maxEnemiesCount;
        [SerializeField] private float minEnemySpawnTimeOut;
        [SerializeField] private float maxEnemySpawnTimeOut;

        public float MinEnemySpeed => minEnemySpeed;
        public float MaxEnemySpeed => maxEnemySpeed;
        public int EnemyStartHealth => enemyStartHealth;
        public int MinEnemiesCount => minEnemiesCount;
        public int MaxEnemiesCount => maxEnemiesCount;
        public float MinEnemySpawnTimeOut => minEnemySpawnTimeOut;
        public float MaxEnemySpawnTimeOut => maxEnemySpawnTimeOut;
    }
}
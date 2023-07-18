using GameLogic.Enemies;
using UnityEngine;

namespace Factories
{
    public class EnemyFactory
    {
        private const string EnemyUnitPath = "Prefabs/Units/EnemyUnit";
        private readonly EnemyUnit _enemyUnitPrefab;

        public EnemyFactory() => 
            _enemyUnitPrefab = Resources.Load<EnemyUnit>(EnemyUnitPath);

        public EnemyUnit GetEnemyUnit(Vector3 spawnPoint, Transform parent) => 
            UnityEngine.Object.Instantiate(_enemyUnitPrefab, spawnPoint, Quaternion.identity, parent);
    }
}
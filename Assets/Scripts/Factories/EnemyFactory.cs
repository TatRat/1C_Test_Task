using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameLogic.Enemies;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class EnemyFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly DiContainer _diContainer;
        private const string EnemyUnitAddress = "EnemyUnit";

        public EnemyFactory(AssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public async UniTask<EnemyUnit> GetEnemyUnit(EnemyModel enemyModel, Vector3 spawnPoint, Transform parent)
        {
            EnemyUnit enemyUnit = _diContainer.InstantiatePrefabForComponent<EnemyUnit>(await _assetProvider.Load<GameObject>(EnemyUnitAddress), spawnPoint, Quaternion.identity, parent);
            enemyUnit.Initialize(enemyModel);
            
            return enemyUnit;
        }
    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Factories;
using GameLogic.Enemies;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GameLogic.GameZones
{
    public class EnemySpawnerView : MonoBehaviour
    {
        public event Action EnemyKilled;

        [SerializeField] private List<Transform> spawnPoints;

        private Transform _enemiesContainer;
        private EnemyFactory _enemyFactory;
        private EnemyConfig _enemyConfig;
        private List<EnemyUnit> _enemies;
        private Coroutine _enemyCreationCoroutine;
        private WaitForSeconds _enemySpawnDelay;

        [Inject]
        void Construct(EnemyFactory enemyFactory) => 
            _enemyFactory = enemyFactory;

        public void Initialize(EnemyConfig enemyConfig, Transform enemiesContainer, float spawnTimeout)
        {
            _enemyConfig = enemyConfig;
            _enemiesContainer = enemiesContainer;

            _enemySpawnDelay = new WaitForSeconds(spawnTimeout);
            _enemies = new List<EnemyUnit>();
            _enemyCreationCoroutine = StartCoroutine(CreateEnemyCoroutine());
        }

        public void Destruct()
        {
            if (_enemyCreationCoroutine != null)
                StopCoroutine(_enemyCreationCoroutine);
            _enemyCreationCoroutine = null;

            foreach (EnemyUnit enemyUnit in _enemies)
            {
                UnSubscribeUnit(enemyUnit);
                enemyUnit.Destruct();
                Destroy(enemyUnit.gameObject);
            }
        }

        private IEnumerator CreateEnemyCoroutine()
        {
            while (true)
            {
                yield return _enemySpawnDelay;
                CreateEnemy();
            }
        }

        private async void CreateEnemy()
        {
            EnemyUnit enemyUnit = await _enemyFactory.GetEnemyUnit(new EnemyModel(_enemyConfig.EnemyStartHealth,
                    Random.Range(_enemyConfig.MinEnemySpeed, _enemyConfig.MaxEnemySpeed)), spawnPoints[Random.Range(0, spawnPoints.Count)].position,
                _enemiesContainer);
                
            enemyUnit.UnitReachedFinish += OnEnemyReachedFinish;
            enemyUnit.UnitDestroyed += OnEnemyDied;

            _enemies.Add(enemyUnit);
        }

        private void OnEnemyDied(EnemyUnit enemyUnit)
        {
            UnSubscribeUnit(enemyUnit);
            _enemies.Remove(enemyUnit);

            EnemyKilled?.Invoke();
        }

        private void OnEnemyReachedFinish(EnemyUnit enemyUnit)
        {
            UnSubscribeUnit(enemyUnit);
            _enemies.Remove(enemyUnit);
        }

        private void UnSubscribeUnit(EnemyUnit enemyUnit)
        {
            enemyUnit.UnitDestroyed -= OnEnemyDied;
            enemyUnit.UnitReachedFinish -= OnEnemyReachedFinish;
        }
    }
}
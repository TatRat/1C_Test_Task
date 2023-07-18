using System;
using UnityEngine;

namespace GameLogic.Enemies
{
    public class EnemyUnit : MonoBehaviour, IDamageable, IEnemy
    {
        public Action<EnemyUnit> UnitDestroyed;
        public Action<EnemyUnit> UnitReachedFinish;

        private int _health;
        private float _speed;

        public void Initialize(EnemyModel enemyModel)
        {
            _health = enemyModel.MaxHealth;
            _speed = enemyModel.Speed;
        }

        private void Update() =>
            transform.position += Time.deltaTime * _speed * Vector3.down;

        public void Destruct()
        {
        }

        public void Damage(int damage)
        {
            _health -= damage;
            if (_health > 0)
                return;

            Death();
        }

        public void Death()
        {
            Destruct();
            UnitDestroyed?.Invoke(this);
            Destroy(this.gameObject);
        }

        public Vector3 GetPosition() => 
            transform.position;

        public void OnFinishReached()
        {
            Destruct();
            UnitReachedFinish?.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}
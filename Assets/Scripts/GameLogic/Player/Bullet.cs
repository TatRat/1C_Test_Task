using GameLogic.Enemies;
using ObjectsPool;
using UnityEngine;

namespace GameLogic.Player
{
    public class Bullet : PoolObject
    {
        [SerializeField] private OverlapDetector overlapDetector;
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private float maxLifetime;
        [SerializeField] private float bulletRadius;

        private float _currentLifetime;
        private Vector3 _direction;
        
        public void Initialize(Vector3 direction)
        {
            _direction = direction;
            overlapDetector.Initialize(bulletRadius);
        }

        void Update()
        {
            transform.position += _direction * (Time.deltaTime * speed);
            
            _currentLifetime += Time.fixedDeltaTime;
            if(maxLifetime <= _currentLifetime)
                Disable();
        }
        
        private void FixedUpdate()
        {
            if(!overlapDetector.FindEnemy(out IDamageable enemy))
                return;
            
            Damage(enemy);
        }

        void Damage(IDamageable enemy)
        {
            enemy.Damage(damage);
            Disable();
        }

        void Disable()
        {
            _direction = Vector3.zero;
            _currentLifetime = 0;
            ReturnToPool();
        }
    }
}
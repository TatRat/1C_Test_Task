using Factories;
using GameLogic.Enemies;
using UnityEngine;

namespace GameLogic.Player
{
    public class AttackHandler
    {
        private readonly Transform _transform;
        private readonly float _cooldown;
        private readonly OverlapDetector _overlapDetector;
        private readonly PlayerFactory _playerFactory;

        private float _currentCooldownTime;
        
        public AttackHandler(Transform transform, float cooldown, OverlapDetector overlapDetector, PlayerFactory playerFactory)
        {
            _transform = transform;
            _cooldown = cooldown;
            _overlapDetector = overlapDetector;
            _playerFactory = playerFactory;
        }

        public void FixedUpdate()
        {
            _currentCooldownTime += Time.fixedDeltaTime;
            if(_currentCooldownTime < _cooldown)
                return;
            
            if(!_overlapDetector.FindEnemy(out IDamageable enemy))
                return;
            
            Attack(enemy);
        }

        private void Attack(IDamageable enemy)
        {
            Bullet bullet = _playerFactory.GetBullet(_transform.position);
            bullet.Initialize(enemy.GetPosition() - _transform.position);
            _currentCooldownTime = 0;
        }
    }
}
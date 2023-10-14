using System;
using GameLogic.Enemies;
using UnityEngine;

namespace GameLogic.GameZones
{
    public class PlayerAreaView : MonoBehaviour
    {
        public event Action EnemyReachedFinish;

        [SerializeField] private BoxCollider2D areaCollider;

        public void Initialize(Vector2 playerAreaRightTopPoint, Vector2 playerAreaLeftDownPoint)
        {
            transform.position = new Vector3(0, playerAreaRightTopPoint.y);

            areaCollider.offset = new Vector2(0, (playerAreaLeftDownPoint.y - playerAreaRightTopPoint.y) / 2);
            areaCollider.size = new Vector2((playerAreaRightTopPoint.x - playerAreaLeftDownPoint.x),
                (playerAreaRightTopPoint.y - playerAreaLeftDownPoint.y));

            areaCollider.enabled = true;
        }

        public void Destruct() =>
            areaCollider.enabled = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IEnemy enemy))
                return;

            enemy.OnFinishReached();
            EnemyReachedFinish?.Invoke();
        }
    }
}
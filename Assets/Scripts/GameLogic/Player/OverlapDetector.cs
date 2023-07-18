using GameLogic.Enemies;
using UnityEngine;

namespace GameLogic.Player
{
    public class OverlapDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        
        private Collider2D[] _resultColliders = new Collider2D[2];
        private float _checkRadius;

        public void Initialize(float checkRadius) => 
            _checkRadius = checkRadius;

        public bool FindEnemy(out IDamageable enemy)
        {
            enemy = null;
            
            if (Check())
                if (_resultColliders[0].TryGetComponent(out enemy))
                    return true;
            
            return false;
        }
        
        private bool Check() => 
            Physics2D.OverlapCircleNonAlloc(transform.position, _checkRadius, _resultColliders, layerMask) > 0;
    }
}
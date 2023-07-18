using UnityEngine;

namespace GameLogic.Enemies
{
    public interface IDamageable
    {
        void Damage(int damage);
        void Death();
        Vector3 GetPosition();
    }
}
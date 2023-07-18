using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float automaticAttackRange;
        [SerializeField] private float attackRate;
        [SerializeField] private float playerSpeed;
        [SerializeField] private int playerStartHealth;

        public float AutomaticAttackRange => automaticAttackRange;
        public float AttackRate => attackRate;
        public float PlayerSpeed => playerSpeed;
        public int PlayerStartHealth => playerStartHealth;
    }
}
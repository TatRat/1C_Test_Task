using UnityEngine;

namespace GameLogic.Player
{
    public class PlayerModel
    {
        private float _speed;
        private float _attackRate;
        private float _attackRange;
        private Vector3 _fieldsRightTopPoint;
        private Vector3 _fieldsLeftDownPoint;

        public float Speed => _speed;
        public float AttackRate => _attackRate;
        public float AttackRange => _attackRange;
        public Vector3 FieldsRightTopPoint => _fieldsRightTopPoint;
        public Vector3 FieldsLeftDownPoint => _fieldsLeftDownPoint;

        public PlayerModel(float speed, float attackRate, float attackRange, Vector3 fieldsRightTopPoint, Vector3 fieldsLeftDownPoint)
        {
            _speed = speed;
            _attackRate = attackRate;
            _attackRange = attackRange;
            _fieldsRightTopPoint = fieldsRightTopPoint;
            _fieldsLeftDownPoint = fieldsLeftDownPoint;
        }
    }
}
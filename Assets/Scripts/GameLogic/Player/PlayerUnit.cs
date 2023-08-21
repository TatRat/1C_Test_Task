using Factories;
using Input;
using UnityEngine;
using Zenject;

namespace GameLogic.Player
{
    public class PlayerUnit : MonoBehaviour
    {
        [SerializeField] private OverlapDetector overlapDetector;
        [SerializeField] private BoxCollider2D boxCollider2D;

        private AttackHandler _attackHandler;
        private IInputService _inputService;
        private PlayerModel _playerModel;
        private float _playerRadius;
        private PlayerFactory _playerFactory;

        [Inject]
        private void Construct(PlayerFactory playerFactory, IInputService inputService)
        {
            _inputService = inputService;
            _playerFactory = playerFactory;
        }

        public void Initialize(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            overlapDetector.Initialize(playerModel.AttackRange);
            _attackHandler = new AttackHandler(transform, playerModel.AttackRate, overlapDetector, _playerFactory);
            _playerRadius = boxCollider2D.bounds.extents.x;
        }

        public void Enable() =>
            _inputService.PlayerInputUpdated += OnInputUpdated;

        public void Disable() =>
            _inputService.PlayerInputUpdated -= OnInputUpdated;

        private void OnInputUpdated(Vector2 input)
        {
            Vector3 position = transform.position + (Vector3)(input * _playerModel.Speed * Time.deltaTime);
            transform.position = new Vector3(
                Mathf.Clamp(position.x, _playerModel.FieldsLeftDownPoint.x + _playerRadius,
                    _playerModel.FieldsRightTopPoint.x - _playerRadius),
                Mathf.Clamp(position.y, _playerModel.FieldsLeftDownPoint.y + _playerRadius,
                    _playerModel.FieldsRightTopPoint.y - _playerRadius), position.z);
        }

        private void FixedUpdate() =>
            _attackHandler.FixedUpdate();
    }
}
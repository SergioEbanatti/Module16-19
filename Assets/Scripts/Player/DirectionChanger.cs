using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class DirectionChanger : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Transform _damagePointTransform;
        [SerializeField] private bool _isPlayerGoRight = true;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            // ������������� _playerMovement � _spriteRenderer, ���� ��� �� ������
            _playerMovement = _playerMovement ? _playerMovement : GetComponent<PlayerMovement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            // �������� ������� _damagePointTransform
            if (_damagePointTransform == null)
                Debug.LogWarning("Damage Point �� �������� � ����������", this);
        }

        private void FixedUpdate()
        {
            CheckDirection();
        }

        private void CheckDirection()
        {
            var currentMoveValue = _playerMovement.CurrentMoveValue;
            var intendedDirection = currentMoveValue >= 0;

            if (_isPlayerGoRight != intendedDirection)
            {
                _isPlayerGoRight = intendedDirection;
                FlipDirection();
                FlipDamagePoint();
            }
        }

        private void FlipDirection()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        private void FlipDamagePoint()
        {
            if (_damagePointTransform == null) return;

            Vector3 currentLocalPosition = _damagePointTransform.localPosition;
            Vector3 invertedLocalPosition = new Vector3(-currentLocalPosition.x, currentLocalPosition.y, currentLocalPosition.z);
            _damagePointTransform.localPosition = invertedLocalPosition;
        }
    }
}
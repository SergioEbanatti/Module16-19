using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Attacker))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement vars")]
        [SerializeField] private float _speed;
        [SerializeField] private float _smoothness;
        [SerializeField] private float _jumpForce;

        [Header("GroundColliderSettings")]
        [SerializeField] private float _collisionOffset;
        [SerializeField] private Transform _colliderTransform;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private LayerMask _wallLayerMask;
        [SerializeField] private bool _isGrounded = false;
        [SerializeField] private bool _isWallBlocking = false;

        [Header("OtherSettings")]
        [SerializeField] private Input _input;
        [SerializeField] private Attacker _attacker;

        [Header("Debug vars")]
        [SerializeField] private float _currentMoveValue;
        [SerializeField] private float _smoothInput;
        [SerializeField] private Vector3 _oldTransformPosition;
        [SerializeField] private bool _isJump = false;
        [SerializeField] private bool _isAttack = false;
        [SerializeField] private bool _isMoving = false;
        [SerializeField] private float _attackCooldown;
        private Coroutine _attackCooldownRoutine;

        private Rigidbody2D _rigidbody;
        private GetAnimationLength _animationLengthComponent;


        #region Свойства
        public float CollisionOffset
        {
            get { return _collisionOffset; }
            set { _collisionOffset = value; }
        }

        public Transform ColliderTransform
        {
            get { return _colliderTransform; }
            set { _colliderTransform = value; }
        }

        public float SmoothInput => _smoothInput;
        public float CurrentMoveValue => _currentMoveValue;

        public bool IsAttack
        {
            get { return _isAttack; }
            set { _isAttack = value; }
        }

        public bool IsJump
        {
            get { return _isJump; }
            set { _isJump = value; }
        }

        public bool IsGrounded
        {
            get { return _isGrounded; }
            set { _isGrounded = value; }
        }
        #endregion

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _oldTransformPosition = transform.position;
            _animationLengthComponent = gameObject.AddComponent<GetAnimationLength>();
            _input.attackEvent.AddListener(OnAttack);
            _input.jumpEvent.AddListener(OnJump);
        }

        private void FixedUpdate()
        {
            IsGrounded = IsGroundedCheck(_groundLayerMask);
            _isWallBlocking = IsGroundedCheck(_wallLayerMask);
            _isMoving = IsMovingCheck();
            OnMove();
        }

        private void OnAttack()
        {
            if (!_isMoving && !_isAttack)
            {
                _isAttack = true;
                _attacker.Attack();

                if (_attackCooldownRoutine != null)
                    StopCoroutine(_attackCooldownRoutine);

                _attackCooldownRoutine = StartCoroutine(AttackCooldown(_animationLengthComponent.GetCurrentAnimationLength()));
            }
        }

        private IEnumerator AttackCooldown(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isAttack = false;
        }

        private void OnJump()
        {
            if (IsGrounded)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                IsJump = true;
            }
            IsJump = false;
        }

        private bool IsGroundedCheck(LayerMask layerMask)
        {
            Vector2 overlapCirclePosition = ColliderTransform.position;
            return Physics2D.OverlapCircle(overlapCirclePosition, CollisionOffset, layerMask);
        }

        private bool IsMovingCheck()
        {
            Vector3 currentPosition = transform.position;
            _isMoving = currentPosition != _oldTransformPosition;
            _oldTransformPosition = currentPosition;

            return _isMoving;
        }

        private void OnMove()
        {
            if (_isWallBlocking && !IsGrounded)
            {
                _smoothInput = -_smoothInput;
                return;
            }

            _smoothInput = Mathf.Lerp(_smoothInput, _input.Move.x, Time.fixedDeltaTime * _smoothness);
            _currentMoveValue = _smoothInput * _speed;
            _rigidbody.velocity = new Vector2(_currentMoveValue, _rigidbody.velocity.y);
        }


    }
}

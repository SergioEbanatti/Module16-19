using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]

    public class Animations : MonoBehaviour
    {
        [SerializeField]
        [Range(0.1f, 0.99f)]
        [Tooltip("Recommended value: 0.1")]
        private float _walkAnimationValue = 0.1f;

        [SerializeField]
        [Range(0.1f, 0.99f)]
        [Tooltip("Recommended value: 0.7")]
        private float _runAnimationValue = 0.7f;

        [SerializeField]
        private PlayerMovement _playerMovement;
        [SerializeField]
        private Health _health;

        private Animator _animator;
        private Rigidbody2D _rigidBody;
        private bool _attackCooldown = false;
        private bool _deathCooldown = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();

            // Проверка на наличие ссылки на PlayerMovement и Health
            _playerMovement = _playerMovement ? _playerMovement : GetComponent<PlayerMovement>();
            _health = _health ? _health : GetComponent<Health>();
        }

        private void FixedUpdate()
        {
            if (!_health.IsPlayerAlive)
            {
                Death();
                return;  // Прекращаем дальнейшее выполнение, если персонаж мертв
            }

            if (_playerMovement.IsJump)
                Jump();

            Attack();
            HorizontalMoveStates();
            VerticalMoveStates();
        }

        private void HorizontalMoveStates()
        {
            float input = _playerMovement.SmoothInput;
            bool isRunning = Mathf.Abs(input) >= _runAnimationValue;
            bool isWalking = Mathf.Abs(input) >= _walkAnimationValue && !isRunning;

            _animator.SetBool("IsRunning", isRunning);
            _animator.SetBool("IsWalking", isWalking);
        }

        private void Jump()
        {
            _animator.SetTrigger("Jump");
        }

        private void VerticalMoveStates()
        {
            float verticalVelocity = _rigidBody.velocity.y;
            bool isGrounded = _playerMovement.IsGrounded;

            _animator.SetBool("IsFlyingUp", verticalVelocity > 0 && !isGrounded);
            _animator.SetBool("IsFalling", verticalVelocity < 0 && !isGrounded);
        }

        private void Attack()
        {
            if (_playerMovement.IsAttack)
            {
                if (_attackCooldown) return;

                _attackCooldown = true;
                _animator.SetTrigger("Attack");
            }
            else
            {
                _attackCooldown = false;
            }
        }

        private void Death()
        {
            if (!_deathCooldown)
            {
                _deathCooldown = true;
                _animator.SetTrigger("Death");
            }
        }
    }
}

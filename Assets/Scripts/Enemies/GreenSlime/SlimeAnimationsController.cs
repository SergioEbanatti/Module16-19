using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Health))]
    public class SlimeAnimationsController : MonoBehaviour
    {

        [HideInInspector] private Health _health;
        [SerializeField] private float _previousHealth;
        [SerializeField] private bool _isDead = false;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _previousHealth = _health.MaxHealth;
        }

        private void Update()
        {
            HealthCheck();
        }

        private void HealthCheck()
        {
            var currentHealth = _health.CurrentHealth;

            if (currentHealth < _previousHealth)
            {
                _previousHealth = currentHealth;
                if (_previousHealth <= 0 && !_isDead)
                    Death();
                else
                    Hurt();
            }

        }

        private void Hurt()
        {
            _animator.SetTrigger("Hurt");
        }

        private void Death()
        {
            _isDead = true;
            _animator.SetBool("Death", true);
        }

    }
}
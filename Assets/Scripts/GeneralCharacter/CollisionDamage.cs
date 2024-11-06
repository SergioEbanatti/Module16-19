
using System.Collections;
using UnityEngine;


namespace Platformer
{

    public class CollisionDamage : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionEffect;   // Эффектор взрыва
        [SerializeField] private float _damage;
        [SerializeField]
        [Tooltip("Recommended value: 0.2")]
        private float _explosionOffDelay = 0.2f;     // Задержка выключения эффекта взрыва
        [SerializeField] private bool _isAttacking = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.CompareTag("Damageable") && _isAttacking == false)
            {
                _isAttacking = true;
                ApplyDamageGlobal.ApplyDamage(collision.transform, _damage);

                if (_explosionEffect != null && TryGetComponent<DelayedDestroy>(out DelayedDestroy delayedDestroy))
                {
                    delayedDestroy.ChangeObjectState(_explosionEffect);
                    StartCoroutine(delayedDestroy.Delay(_explosionEffect, _explosionOffDelay, newAttackState =>
                    _isAttacking = newAttackState));
                }

            }
        }

    }
}
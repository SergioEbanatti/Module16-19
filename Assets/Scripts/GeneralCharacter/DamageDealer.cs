
using System.Collections;
using UnityEngine;


namespace Platformer
{

    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField]
        [Tooltip("Recommended value: 0.1")]
        private float _destroyDelay = 0.1f;     // Задержка уничтожения объекта

        private void Start()
        {
            Destroy(gameObject, _destroyDelay);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_damage > 0)
            {
                Transform damageableTransform = FindDamageableParent(collision.transform);

                if (damageableTransform != null)
                    ApplyDamageGlobal.ApplyDamage(damageableTransform, _damage);
            }
        }

        // Подъем по иерархии для поиска "Damageable" родителя
        private Transform FindDamageableParent(Transform startTransform)
        {
            Transform currentTransform = startTransform;

            while (currentTransform != null)
            {
                if (currentTransform.CompareTag("Damageable"))
                    return currentTransform;

                currentTransform = currentTransform.parent;
            }

            return null; // Если объект с тегом не найден
        }

    }

}
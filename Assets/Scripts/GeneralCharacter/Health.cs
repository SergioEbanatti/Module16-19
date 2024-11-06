using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{

    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private bool _isPlayerAlive;

        #region Свойства
        public float CurrentHealth
        {
            get { return _currentHealth; }
            private set
            {
                _currentHealth = Mathf.Max(value, 0); // Убеждаемся, что здоровье не меньше 0
                CheckIsAlive(); // Проверяем состояние живости при изменении здоровья
            }
        }

        public float MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        public bool IsPlayerAlive => _isPlayerAlive;

        #endregion

        private void Awake()
        {
            CurrentHealth = MaxHealth;
            _isPlayerAlive = true;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            CheckIsAlive();
        }

        private void CheckIsAlive()
        {
            if (CurrentHealth <= 0)
            {
                if (TryGetComponent<DeathNPC>(out DeathNPC deathNPC))
                    deathNPC.Death();
                else if (TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
                    _isPlayerAlive = false;
            }
        }
    }
}
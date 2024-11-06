using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{

    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private bool _isPlayerAlive;

        #region ��������
        public float CurrentHealth
        {
            get { return _currentHealth; }
            private set
            {
                _currentHealth = Mathf.Max(value, 0); // ����������, ��� �������� �� ������ 0
                CheckIsAlive(); // ��������� ��������� ������� ��� ��������� ��������
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
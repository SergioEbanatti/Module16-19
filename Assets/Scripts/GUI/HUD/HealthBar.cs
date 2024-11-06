using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    [RequireComponent(typeof(Image))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _healthScript;
        [SerializeField] private Sprite[] _healthSprites;

        private Image _imageComponent;

        private void Awake()
        {
            _imageComponent = GetComponent<Image>();

            // Ќачальное обновление полоски здоровь€
            if (_healthScript != null)
                UpdateHealthBar(_healthScript.CurrentHealth);
            else
                Debug.LogError("Health скрипт не добавлен в инспекторе");
        }

        private void Update()
        {
            if (_healthScript != null)
                UpdateHealthBar(_healthScript.CurrentHealth);
        }

        private void UpdateHealthBar(float currentHealth)
        {

            if (_healthScript == null) return;

            float healthPercentage = currentHealth / _healthScript.MaxHealth;
            int spriteIndex = Mathf.Clamp(Mathf.FloorToInt(healthPercentage * _healthSprites.Length), 0, _healthSprites.Length - 1);
            _imageComponent.sprite = _healthSprites[spriteIndex];
        }

    }
}

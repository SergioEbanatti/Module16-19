using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Platformer
{

    public class DeathSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _defeatPanel;
        [SerializeField] private GlobalTimeScale _globalTimeScale;
        [SerializeField] private GameObject _inputs;
        [SerializeField] private float _delay = 1f;

        [SerializeField] private PauseSystemController _pauseSystemController;
        [SerializeField] private Health _playerHealth;
        [SerializeField] private bool _deathCooldown = false;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
            if (!_playerHealth.IsPlayerAlive)
                PlayerIsDead();     
        }

        /// <summary>
        /// Активирует анимацию смерти
        /// </summary>
        public void PlayerIsDead()
        {
            if (!_deathCooldown)
            {
                _deathCooldown = true;
                _inputs.TryGetComponent<Input>(out Input input);
                input.enabled = false;
                Invoke(nameof(DeathMenu), _delay);        //Открываем экран смерти с задержкой
            }

        }

        /// <summary>
        /// Активирует меню экрана смерти
        /// </summary>
        private void DeathMenu()
        {
            _pauseSystemController.DefeatScreenOn();    
        }
    }
}
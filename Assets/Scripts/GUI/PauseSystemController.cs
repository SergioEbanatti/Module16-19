using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Platformer
{


    public class PauseSystemController : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _defeatPanel;
        [SerializeField] private GlobalTimeScale _globalTimeScale;
        [SerializeField] private GameObject _inputObject;

        private Controls _inputControls;
        public bool isPlayerWin;

        private void Awake()
        {
            _inputControls = new Controls();
        }


        private void OnEnable()
        {
            _inputControls.UI.Enable();
            _inputControls.UI.PauseMenu.started += OnPauseButtonPressed;
        }

        private void OnDisable()
        {
            _inputControls.UI.PauseMenu.started -= OnPauseButtonPressed;
            _inputControls.UI.Disable();
        }

        public void DefeatScreenOn()
        {
            if (isPlayerWin) return;

            var isPaused = IsGamePaused();
            ChangeTimeScale(isPaused);
            _defeatPanel.SetActive(!isPaused);
            _inputObject.SetActive(isPaused);
        }

        private bool IsGamePaused()
        {
            return _globalTimeScale.IsGamePaused;
        }

        private void ChangeTimeScale(bool isPaused)
        {
            _globalTimeScale.PauseGame(!isPaused);
        }

        public void RestartButtonOnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnPauseButtonPressed(InputAction.CallbackContext context)
        {
            PauseButtonOnClick();
        }

        public void PauseButtonOnClick()
        {
            if (isPlayerWin) return;

            var isPaused = IsGamePaused();
            ChangeTimeScale(isPaused);
            _pausePanel.SetActive(!isPaused);
            _inputObject.SetActive(isPaused);
        }

        public void ToMainMenuButtonOnClick()
        {
            EventSystemManager.Instance.EnsureSingleEventSystem();  // Убедимся, что только один EventSystem
            ChangeTimeScale(!IsGamePaused());
            SceneManager.LoadScene(0);
        }
    }
}
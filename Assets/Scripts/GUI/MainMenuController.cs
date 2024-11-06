using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{


    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _levelSelectPanel;

        public void ExitButtonOnClick()
        {
            Application.Quit();
        }

        public void SettingsButtonOnClick()
        {
            TogglePanel(_settingsPanel);
        }

        public void ToMainMenuButtonOnClick()
        {
            TogglePanel(_mainMenuPanel);
        }

        public void LevelSelectButtonOnClick()
        {
            TogglePanel(_levelSelectPanel);
        }

        public void LevelOneLoadButton()
        {
            EventSystemManager.Instance.EnsureSingleEventSystem();  // Убедимся, что только один EventSystem
            LoadLevel(1);
        }

        private void LoadLevel(int levelIndex)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(levelIndex);
        }

        private void TogglePanel(GameObject panelToActivate)
        {
            _mainMenuPanel.SetActive(panelToActivate == _mainMenuPanel);
            _settingsPanel.SetActive(panelToActivate == _settingsPanel);
            _levelSelectPanel.SetActive(panelToActivate == _levelSelectPanel);
        }
    }
}
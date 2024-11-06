using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{


    public class GlobalTimeScale : MonoBehaviour
    {

        public bool IsGamePaused { get; private set; }

        private void Awake()
        {
            PauseGame(false);
        }

        /// <summary>
        /// —тавит игру на паузу
        /// </summary>
        /// <param name="value">true - пауза. false - сн€ть с паузы</param>
        public void PauseGame(bool value)
        {
            IsGamePaused = value;
            Time.timeScale = (value) ? 0.0f : 1.0f;
        }
    }
}
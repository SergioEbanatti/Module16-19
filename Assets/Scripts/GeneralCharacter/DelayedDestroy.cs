using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class DelayedDestroy : MonoBehaviour
    {

        // Метод для переключения состояния объекта
        public void ChangeObjectState(GameObject gameObject)
        {
            bool isActive = gameObject.activeSelf;
            gameObject.SetActive(!isActive);
        }

        public IEnumerator Delay(GameObject gameObject, float delay, System.Action<bool> callback = null)
        {
            var isCoroutineActive = true;
            yield return new WaitForSeconds(delay);
            ChangeObjectState(gameObject);
            callback?.Invoke(!isCoroutineActive);
        }
    }
}

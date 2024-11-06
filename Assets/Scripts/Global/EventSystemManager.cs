using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemManager : MonoBehaviour
{
    public static EventSystemManager Instance { get; private set; }

    private void Awake()
    {
        // Реализуем паттерн Singleton, чтобы гарантировать единственный экземпляр этого класса
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Сохраняем объект между сценами
        }
        else
            Destroy(gameObject);  // Удаляем дубликаты

        EnsureSingleEventSystem();  // Убедимся, что в сцене только один EventSystem
    }

    private void OnEnable()
    {
        // Подписка на событие, которое срабатывает при смене сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Отписка от события при деактивации объекта
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Каждый раз при загрузке новой сцены проверяем наличие дублирующихся EventSystem
        EnsureSingleEventSystem();
    }

    // Метод для проверки и удаления лишних EventSystem
    public void EnsureSingleEventSystem()
    {
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)  // Начинаем с 1, чтобы оставить первый экземпляр
                Destroy(eventSystems[i].gameObject);  // Удаляем лишние EventSystem

        }
    }
}

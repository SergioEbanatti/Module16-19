using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EventSystemManager : MonoBehaviour
{
    public static EventSystemManager Instance { get; private set; }

    private void Awake()
    {
        // ��������� ������� Singleton, ����� ������������� ������������ ��������� ����� ������
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ��������� ������ ����� �������
        }
        else
            Destroy(gameObject);  // ������� ���������

        EnsureSingleEventSystem();  // ��������, ��� � ����� ������ ���� EventSystem
    }

    private void OnEnable()
    {
        // �������� �� �������, ������� ����������� ��� ����� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // ������� �� ������� ��� ����������� �������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ������ ��� ��� �������� ����� ����� ��������� ������� ������������� EventSystem
        EnsureSingleEventSystem();
    }

    // ����� ��� �������� � �������� ������ EventSystem
    public void EnsureSingleEventSystem()
    {
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)  // �������� � 1, ����� �������� ������ ���������
                Destroy(eventSystems[i].gameObject);  // ������� ������ EventSystem

        }
    }
}

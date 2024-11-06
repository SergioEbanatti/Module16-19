using UnityEditor;
using UnityEngine;

public class RenameChildrenObjects : EditorWindow
{
    public string newNamePrefix = "NewObject"; // Префикс для новых имен
    public int startIndex = 0; // Начальный индекс

    [MenuItem("Tools/Rename Children Objects")]
    static void Init()
    {
        RenameChildrenObjects window = (RenameChildrenObjects)EditorWindow.GetWindow(typeof(RenameChildrenObjects));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Rename Children Objects", EditorStyles.boldLabel);

        newNamePrefix = EditorGUILayout.TextField("New Name Prefix:", newNamePrefix);
        startIndex = EditorGUILayout.IntField("Start Index:", startIndex);

        if (GUILayout.Button("Rename Children"))
        {
            Rename();
        }
    }

    void Rename()
    {
        Transform[] children = Selection.activeGameObject.GetComponentsInChildren<Transform>();

        int index = startIndex;

        foreach (Transform child in children)
        {
            if (child != Selection.activeTransform)
            {
                string newName = newNamePrefix + index;
                child.gameObject.name = newName;
                index++;
            }
        }
    }
}

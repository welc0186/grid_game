using UnityEngine;
using UnityEditor;

public class GameGridSpawnerWindow : EditorWindow
{
    int _width;
    int _height;

    [MenuItem("Custom/Game Grid Spawner Window")]
    static void Init()
    {
        // Get existing open window or if none, create a new one
        GameGridSpawnerWindow window = (GameGridSpawnerWindow)EditorWindow.GetWindow(typeof(GameGridSpawnerWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Game Grid Spawner", EditorStyles.boldLabel);

        _width = EditorGUILayout.IntField("Grid Width", _width);
        _height = EditorGUILayout.IntField("Grid Height", _height);

        // Spawn button
        if (GUILayout.Button("Spawn Grid"))
        {
            SpawnGrid();
        }
    }

    void SpawnGrid()
    {
        if (_width > 0 && _height > 0)
        {
            GameGridSpawner.SpawnPrefab(_width, _height);
        }
        else
        {
            Debug.LogError("Game Grid Width and Height must be positive integers");
        }
    }
}

using UnityEngine;
using UnityEditor;

public class SpawnPrefabWindow : EditorWindow
{
    GameObject prefabToSpawn;

    [MenuItem("Custom/Spawn Prefab Window")]
    static void Init()
    {
        // Get existing open window or if none, create a new one
        SpawnPrefabWindow window = (SpawnPrefabWindow)EditorWindow.GetWindow(typeof(SpawnPrefabWindow));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Prefab Spawner", EditorStyles.boldLabel);

        // Prefab selection
        prefabToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", prefabToSpawn, typeof(GameObject), false) as GameObject;

        // Spawn button
        if (GUILayout.Button("Spawn Prefab"))
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            // Instantiate prefab at the scene view's center
            Vector3 spawnPosition = SceneView.lastActiveSceneView.camera.transform.position + SceneView.lastActiveSceneView.camera.transform.forward * 5f;
            GameObject spawnedPrefab = PrefabUtility.InstantiatePrefab(prefabToSpawn) as GameObject;
            spawnedPrefab.transform.position = spawnPosition;
            Selection.activeObject = spawnedPrefab;
        }
        else
        {
            Debug.LogError("Prefab not assigned. Please assign a prefab to spawn.");
        }
    }
}

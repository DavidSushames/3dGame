// Editor/ScatterTrees.cs
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScatterTrees : EditorWindow
{
    GameObject[] prefabs = new GameObject[0];
    int count = 100;
    float radius = 50f;
    Transform center;

    [MenuItem("Tools/Scatter Trees")]
    static void Open() => GetWindow<ScatterTrees>("Scatter Trees");

    void OnGUI()
    {
        GUILayout.Label("Prefabs");
        int newCount = Mathf.Max(0, EditorGUILayout.IntField("Size", prefabs.Length));
        if (newCount != prefabs.Length)
        {
            System.Array.Resize(ref prefabs, newCount);
        }
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i] = (GameObject)EditorGUILayout.ObjectField("  " + i, prefabs[i], typeof(GameObject), false);
        }

        EditorGUILayout.Space();
        center = (Transform)EditorGUILayout.ObjectField("Center", center, typeof(Transform), true);
        count = EditorGUILayout.IntField("Count", count);
        radius = EditorGUILayout.FloatField("Radius", radius);

        if (GUILayout.Button("Scatter") && prefabs.Length > 0 && center != null)
            Scatter();
    }

    void Scatter()
    {
        List<Vector3> placed = new List<Vector3>();
        float minDistance = 2f; // minimum gap between trees, adjust to taste

        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
            if (prefab == null) continue;

            Vector3 pos = Vector3.zero;
            bool found = false;

            // Try several times to find a valid non-overlapping spot
            for (int attempt = 0; attempt < 20; attempt++)
            {
                float x = UnityEngine.Random.Range(-radius, radius);
                float z = UnityEngine.Random.Range(-radius, radius);
                pos = center.position + new Vector3(x, 0f, z);

                bool tooClose = false;
                foreach (Vector3 p in placed)
                {
                    if (Vector3.Distance(pos, p) < minDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose) { found = true; break; }
            }

            if (!found) continue;

            placed.Add(pos);
            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
            Undo.RegisterCreatedObjectUndo(obj, "Scatter Tree");
        }
    }
}
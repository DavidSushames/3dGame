// Editor/RandomiseTreeRotations.cs
using System;
using UnityEditor;
using UnityEngine;

public class RandomiseTreeRotations : EditorWindow
{
    [MenuItem("Tools/Randomise Tree Rotations")]
    static void Run()
    {
        Terrain terrain = FindObjectOfType<Terrain>();
        if (terrain == null) { Debug.LogError("No terrain found."); return; }

        TerrainData data = terrain.terrainData;
        TreeInstance[] trees = data.treeInstances;

        for (int i = 0; i < trees.Length; i++)
        {
            trees[i].rotation = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        }

        data.treeInstances = trees;
        EditorUtility.SetDirty(terrain);
        Debug.Log($"Randomised {trees.Length} trees.");
    }
}
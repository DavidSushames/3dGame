// RandomRotateOnStart.cs
using System;
using UnityEngine;

public class RandomRotateOnStart : MonoBehaviour
{
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
    }
}
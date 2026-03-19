using System.Globalization;
using UnityEngine;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerData playerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerData.health = 100; 
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + playerData.health;
    }
}

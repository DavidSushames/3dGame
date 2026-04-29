using System.Globalization;
using UnityEngine;
using TMPro;

public class InfoManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public PlayerData playerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerData.health = 100;
        playerData.kills = 0;
        playerData.coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        playerData.score = playerData.kills + playerData.coins;
        healthText.text = "HP: " + playerData.health;
        killText.text = "Kills: " + playerData.kills;
        coinText.text = "Coins: " + playerData.coins;
        scoreText.text = "Score: " + playerData.score;
    }
}

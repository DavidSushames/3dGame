using UnityEngine;

public class WinLose : MonoBehaviour
{
    public GameObject Win;
    public GameObject Lose;
    public PlayerData playerData;
    public int killGoal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Win.gameObject.SetActive(false);
        Lose.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.kills >= killGoal)
        {
            Win.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        else if (playerData.health <= 0)
        {
            Lose.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}

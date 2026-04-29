using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Security.Permissions;

public class WinLose : MonoBehaviour
{
    public GameObject Win;
    public GameObject Lose;
    public GameObject Score;
    public PlayerData playerData;
    public int killGoal;
    public InputActionReference endless;

    void Start()
    {
        Win.gameObject.SetActive(false);
        Lose.gameObject.SetActive(false);
        Score.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerData.kills >= killGoal)
        {
            Win.gameObject.SetActive(true);
            Score.gameObject.SetActive(true);
            Time.timeScale = 0;

            if (endless.action.WasPressedThisFrame())
            {
                Win.gameObject.SetActive(false);
                Score.gameObject.SetActive(false);
                killGoal = 9999;
                Time.timeScale = 1;
            }
        }
        else if (playerData.health <= 0)
        {
            Lose.gameObject.SetActive(true);
            Score.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
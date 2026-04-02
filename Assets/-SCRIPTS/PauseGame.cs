using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    //public Toggle PauseButton;
    //public PlayerData playerdata;
    public InputActionReference pause;

    bool Pause;


    // Start is called before the first frame update
    void Start()
    {
        Pause = false;
        Time.timeScale = 1;
        //PauseButton.isOn = false;
    }
        
    // Update is called once per frame
    void Update()
    {
        if (pause.action.WasPressedThisFrame() && Pause == false)
        {
            Debug.Log("Pause Pressed");
            Time.timeScale = 0;
            Pause = true;
        }

        else if (pause.action.WasPressedThisFrame() && Pause == true)
        {
            Debug.Log("Pause Unpressed");
            Time.timeScale = 1;
            Pause = false;
        }
    }
}

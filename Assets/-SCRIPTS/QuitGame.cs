using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class QuitGame : MonoBehaviour
{

    public string GameScene;
    public InputActionReference menu;

    // Start is called before the first frame update
    void Start()
    {

    }
    void TaskOnClickQuit()
    {
        Debug.Log("You have clicked Quit Button!");
        
    }
    // Update is called once per frame
    void Update()
    {
        if (menu.action.WasPressedThisFrame())
        {
            SceneManager.LoadScene(GameScene);
        }
    }
}

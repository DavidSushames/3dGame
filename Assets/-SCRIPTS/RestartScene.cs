using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class RestartScene : MonoBehaviour

    
{   //public Button RestartButton;
    public InputActionReference restart;
    
    // Start is called before the first frame update
    void Start()
    {
       // Button btn6 = RestartButton.GetComponent<Button>();
       // btn6.onClick.AddListener(TaskOnClick6);
    }

    void TaskOnClick6()
    {
        Debug.Log("You have clicked RESET!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

     

    // Update is called once per frame
    void Update()
    {
        if (restart.action.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //public Toggle Soundtoggle;
    public InputActionReference mute;

    bool Mute;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Soundtoggle.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mute.action.WasPressedThisFrame() && Mute == false)
        {
            Debug.Log("Mute Pressed");
            AudioListener.volume = 0;
            Mute = true;
        }

        else if (mute.action.WasPressedThisFrame() && Mute == true)
        {
            Debug.Log("Mute Unpressed");
            AudioListener.volume = 1;
            Mute = false;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Toggle musicToggle;


    void Start()
    {
        musicToggle.isOn = DontDestroyAudio.Instance.shouldPlay;
        musicSlider.value = 0.3f;

        //Adds a listener to the main slider and invokes a method when the value changes.
        musicSlider.onValueChanged.AddListener(delegate { SliderValueChangeCheck(); });
        musicToggle.onValueChanged.AddListener (delegate { ToggleValueChangeCheck(); });
    }


    public void ToggleValueChangeCheck()
    {
        if (musicToggle.isOn) 
        {
            DontDestroyAudio.Instance.GetComponent<AudioSource>().Play();
            DontDestroyAudio.Instance.shouldPlay = true;
            print("ON");
        }
        else
        {
            DontDestroyAudio.Instance.GetComponent<AudioSource>().Pause();
            DontDestroyAudio.Instance.shouldPlay = false;
            print("OFF");
        }
    }


    // Invoked when the value of the slider changes.
    public void SliderValueChangeCheck()
    {
        DontDestroyAudio.Instance.GetComponent<AudioSource>().volume = musicSlider.value;
    }


    public void SaveButton()
    {
        SceneManager.LoadScene(0);
    }

}

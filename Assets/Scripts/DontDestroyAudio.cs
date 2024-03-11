using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAudio : MonoBehaviour
{
    AudioSource audioSource;

    public static DontDestroyAudio Instance { get; private set; }

    public bool shouldPlay;
    public bool toggleChange;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            shouldPlay = true;
            toggleChange = true;
        }
    }


    private void Update()
    {
        if (shouldPlay && toggleChange) 
        {
            //Play the audio you attach to the AudioSource component
            audioSource.Play();

            //Ensure audio doesn’t play more than once
            toggleChange = false;
        }

        //Check if you just set the toggle to false
        if (shouldPlay == false && toggleChange == true)
        {
            //Stop the audio
            audioSource.Stop();
            //Ensure audio doesn’t play more than once
            toggleChange = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the UI slider

    private AudioSource audioSource; // Reference to the AudioSource component

    void Awake()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // If the AudioSource component is not found, log a warning
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on GameObject: ");
        }

        // Ensure that this GameObject persists between scene changes
        DontDestroyOnLoad(audioSource);
    }

    void Start()
    {
        // If the volume slider reference is not set, log a warning
        if (volumeSlider == null)
        {
            Debug.LogWarning("Volume Slider reference not set in AudioVolumeControl script on GameObject: " );
            return;
        }

        // Set the slider value to the initial volume of the audio source
        volumeSlider.value = audioSource.volume;

        // Add a listener to the slider to detect changes in its value
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    // Method called when the slider value changes
    void OnVolumeChanged(float volume)
    {
        // Update the volume of the audio source
        audioSource.volume = volume;
    }
}


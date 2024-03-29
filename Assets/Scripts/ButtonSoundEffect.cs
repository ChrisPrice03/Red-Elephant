using UnityEngine;

public class ButtonSoundEffect : MonoBehaviour
{
    public AudioSource audio1;

    void Awake()
    {
        // This will prevent the gameObject from being destroyed when loading a new scene
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayAudioClip()
    {
        audio1.Play();
    }
}

using UnityEngine;

public class VoiceLinePlayer : MonoBehaviour
{
    // Array of audio clips to play
    public AudioClip[] voiceLines;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Reference to the Rigidbody2D component for movement checking
    private Rigidbody2D rb;

    void Start()
    {
        // Get the AudioSource component from the GameObject this script is attached to
        audioSource = GetComponent<AudioSource>();

        // Get the Rigidbody2D component from the GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if 'V' is pressed and player is not moving (velocity is zero)
        if (Input.GetKeyDown(KeyCode.V) && rb.velocity == Vector2.zero)
        {
            PlayRandomVoiceLine();
        }
    }

    void PlayRandomVoiceLine()
    {
        if (voiceLines.Length > 0)
        {
            // Select a random clip from the voiceLines array
            int index = Random.Range(0, voiceLines.Length);
            AudioClip clip = voiceLines[index];

            // Play the selected clip
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}

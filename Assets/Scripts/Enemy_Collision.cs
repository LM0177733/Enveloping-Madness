using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy_Collision : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the AudioSource used for playing looping 3D sound effects.
    /// </summary>
    /// <remarks>This AudioSource should be configured for 3D spatial audio and set to loop for continuous
    /// playback. Assign an AudioSource component that is properly positioned in the scene to achieve the desired
    /// spatial effect.</remarks>
    [Header("Audio Settings")]
    public AudioSource looping3DSound;
    public AudioSource killSoundSource;

    private bool isEnding = false;
    /// <summary>
    /// Handles collision events and initiates the game over sequence when the colliding object is the player and the
    /// game is not already ending.
    /// </summary>
    /// <param name="collision">The collision information associated with the contact event, including details about the colliding object.</param>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isEnding)
        {
            StartCoroutine(GameOverSequence());
        }
    }
    /// <summary>
    /// Performs the game over sequence, including stopping active sounds, playing the kill sound effect if available,
    /// and transitioning to the game over scene.
    /// </summary>
    /// <remarks>This coroutine should be started when the game ends to ensure that audio cues and scene
    /// transitions occur in the correct order. The method waits for the duration of the kill sound effect, if present,
    /// before loading the game over scene.</remarks>
    /// <returns>An enumerator that controls the timing of the game over sequence and scene transition.</returns>

    IEnumerator GameOverSequence()
    {
        isEnding = true;
        if (looping3DSound != null)
        {
            looping3DSound.Stop();// Stop the looping 3D sound effect
        }
        if (killSoundSource != null && killSoundSource.clip != null)
        {
            killSoundSource.Play();// Play the kill sound effect
            yield return new WaitForSeconds(killSoundSource.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene("Game_Over");
    }
}

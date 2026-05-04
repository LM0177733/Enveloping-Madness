using UnityEngine;

public class Player_Effects : MonoBehaviour
{
    public ParticleSystem blood;
    private AudioSource audio_source;
    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }
    // This method plays the hit effect, which can be either a blood effect or a spark effect, depending on the context of the hit. It ensures that the effect is stopped before playing it again to allow for repeated hits to trigger the effect properly.
    public void play_hit_effect()
    {
        if (blood != null)
        {
            blood.Play();
        }
        else
        {
            Debug.LogError("blood effect is missing from the Inspector!");
        }
        if (audio_source != null)
        {
            audio_source.Play();
        }
    }
}

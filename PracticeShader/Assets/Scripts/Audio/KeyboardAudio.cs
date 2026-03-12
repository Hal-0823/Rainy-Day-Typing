using UnityEngine;
using GameConfig;

public class KeyboardAudio : MonoBehaviour
{
    [SerializeField]
    private SEConfig seConfig;

    [SerializeField]
    private AudioSource audioSource;

    public void PlayRandomKeySE()
    {
        int randomIndex = Random.Range(0, seConfig.GetKeySECount());
        audioSource.clip = seConfig.GetAudioClip(randomIndex);
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}

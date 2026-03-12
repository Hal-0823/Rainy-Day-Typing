using UnityEngine;
using GameConfig;

public class KeyboardAudio : MonoBehaviour
{
    [SerializeField]
    private SEConfig seConfig;

    [SerializeField]
    private AudioSource audioSource;

    private void OnEnable()
    {
        TypingManager.OnTypeCorrect += PlayRandomKeySE;
    }

    private void OnDisable()
    {
        TypingManager.OnTypeCorrect -= PlayRandomKeySE;
    }

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

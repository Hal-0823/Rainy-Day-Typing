using UnityEngine;

public class KnockAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip knockSE;

    private void OnEnable()
    {
        GameManager.OnKnock += PlayKnockSE;
    }

    private void OnDisable()
    {
        GameManager.OnKnock -= PlayKnockSE;
    }

    private void PlayKnockSE()
    {
        if (audioSource != null && knockSE != null)
        {
            audioSource.clip = knockSE;
            audioSource.Play();
        }
    }
}
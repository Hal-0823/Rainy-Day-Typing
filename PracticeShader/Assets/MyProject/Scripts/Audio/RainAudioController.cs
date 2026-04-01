using UnityEngine;

public class RainAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _rainClip;
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントが見つかりません。", this);
        }
    }

    public void PlayRainSound()
    {
        _audioSource.clip = _rainClip;
        _audioSource.Play();
    }
}

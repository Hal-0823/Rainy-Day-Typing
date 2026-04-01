using UnityEngine;

public class SystemAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _sceneTransitionSE;
    [SerializeField] private AudioClip _buttonClickSE;
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントが見つかりません。", this);
        }
    }

    public void PlaySceneTransitionSE()
    {
        _audioSource.clip = _sceneTransitionSE;
        _audioSource.Play();
    }

    public void PlayButtonClickSE()
    {
        _audioSource.clip = _buttonClickSE;
        _audioSource.Play();
    }
}

using UnityEngine;

public class KeyboardAudioController : MonoBehaviour
{
    [SerializeField] private KeyboardSE _keyboardSE;
    
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントが見つかりません。", this);
        }
    }

    public void PlayRandomKeySE()
    {
        int randomIndex = Random.Range(0, _keyboardSE.GetKeySECount());
        _audioSource.clip = _keyboardSE.GetAudioClip(randomIndex);
        _audioSource.Play();
    }
}

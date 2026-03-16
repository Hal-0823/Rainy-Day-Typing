using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private KeyboardSE _keyboardSE;

    public MusicAudioController MusicAudioController { get; private set; }
    public KeyboardAudioController KeyboardAudioController { get; private set; }
    public RainAudioController RainAudioController { get; private set; }
    public SystemAudioController SystemAudioController { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MusicAudioController = GetComponentInChildren<MusicAudioController>();
        KeyboardAudioController = GetComponentInChildren<KeyboardAudioController>();
        RainAudioController = GetComponentInChildren<RainAudioController>();
        SystemAudioController = GetComponentInChildren<SystemAudioController>();
    }

    public void SetMusicVolume(int volume)
    {
        SetVolume("MusicVolume", volume);
    }

    public void SetKeyboardVolume(int volume)
    {
        SetVolume("KeyboardVolume", volume);
    }

    public void SetRainVolume(int volume)
    {
        SetVolume("RainVolume", volume);
    }

    public void SetKeyboardSEType(KeyboardSE.Type type)
    {
        _keyboardSE.SetSelectedType(type);
    }
    
    private void SetVolume(string parameterName, int volume)
    {
        _audioMixer.SetFloat(parameterName, Mathf.Clamp(Mathf.Log10(volume / 10f) * 20, -80f, 0f));
    }
}
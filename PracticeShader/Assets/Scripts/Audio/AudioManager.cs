using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private KeyboardSE _keyboardSE;

    [SerializeField] private KeyboardAudioController _keyboardAudioController;
    public KeyboardAudioController KeyboardAudioController => _keyboardAudioController;

    public void SetBGMVolume(int volume)
    {
        SetVolume("BGMVolume", volume);
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
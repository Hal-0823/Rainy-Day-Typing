using UnityEngine;
using UnityEngine.UI;
using R3;
using UnityEngine.InputSystem;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private VolumeSliderWrapper _musicVolumeSlider;
    [SerializeField] private VolumeSliderWrapper _rainVolumeSlider;
    [SerializeField] private VolumeSliderWrapper _keyboardVolumeSlider;

    [SerializeField] private ShiftContextWrapper _shiftContextWrapper;

    public Observable<int> OnMusicVolumeChanged => _musicVolumeSlider.OnVolumeChanged;
    public Observable<int> OnRainVolumeChanged => _rainVolumeSlider.OnVolumeChanged;
    public Observable<int> OnKeyboardVolumeChanged => _keyboardVolumeSlider.OnVolumeChanged;

    public void SetMusicVolume(int volume) => _musicVolumeSlider.SetSliderValue(volume);
    public void SetRainVolume(int volume) => _rainVolumeSlider.SetSliderValue(volume);
    public void SetKeyboardVolume(int volume, bool playSE = true)
    {
        _keyboardVolumeSlider.SetSliderValue(volume);
        if (playSE)
        {
            AudioManager.Instance.KeyboardAudioController.PlayRandomKeySE();
        }
    }

    public Observable<Unit> OnPressedTabButton =>
        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Tab))
            .Select(_ => Unit.Default);

    public Observable<Unit> OnPressedLeftShiftThemeButton => _shiftContextWrapper.OnPressedLeftShiftButton;
    public Observable<Unit> OnPressedRightShiftThemeButton => _shiftContextWrapper.OnPressedRightShiftButton;

    public void ChangeMenuVisibility(bool isVisible)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }
    }

    public void SetCurrentTheme(string themeName)
    {
        _shiftContextWrapper.SetCurrentContext(themeName);
    }
}
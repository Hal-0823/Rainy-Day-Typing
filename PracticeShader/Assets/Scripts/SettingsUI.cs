using UnityEngine;
using UnityEngine.Rendering;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsCanvas;

    [SerializeField]
    private Volume volume;

    [SerializeField]
    private VolumeProfile blurVolumeProfile;

    private VolumeProfile originalVolumeProfile;

    private void Awake()
    {
        settingsCanvas.SetActive(false);
    }

    public void ShowSettings()
    {
        originalVolumeProfile = volume.sharedProfile;
        volume.sharedProfile = blurVolumeProfile;
        settingsCanvas.SetActive(true);
    }

    public void HideSettings()
    {
        volume.sharedProfile = originalVolumeProfile;
        settingsCanvas.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.Rendering;

public class RenderVolumeController : MonoBehaviour
{
    [SerializeField] Volume _volume;
    [SerializeField] VolumeProfile _defaultProfile;
    [SerializeField] VolumeProfile _blurProfile;

    private void Awake()
    {
        _volume.profile = _defaultProfile;
    }

    public void SetBlur(bool isBlur)
    {
        _volume.profile = isBlur ? _blurProfile : _defaultProfile;
    }
}

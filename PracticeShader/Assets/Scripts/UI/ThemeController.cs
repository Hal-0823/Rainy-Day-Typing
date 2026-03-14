using UnityEngine;

public class ThemeController : MonoBehaviour
{
    [SerializeField] private Theme _theme;

    public void SetTheme(Theme.Type theme)
    {
        switch (theme)
        {
            case Theme.Type.SettingSun:
                RenderSettings.skybox = _theme.GetSkybox(Theme.Type.SettingSun);
                break;
            case Theme.Type.HeavyOvercast:
                RenderSettings.skybox = _theme.GetSkybox(Theme.Type.HeavyOvercast);
                break;
        }
    }
}
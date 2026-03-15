using UnityEngine;

[System.Serializable]
public class GameConfig
{
    public int RainVolume = 10;
    public int KeyboardVolume = 10;
    public int BGMVolume = 10;

    public KeyboardSE.Type SelectedKeyboardType = KeyboardSE.Type.Mechanical;
    public ThemeCatalog.Type SelectedTheme = (ThemeCatalog.Type)0;
}
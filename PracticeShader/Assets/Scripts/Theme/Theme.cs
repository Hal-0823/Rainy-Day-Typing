using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "ScriptableObjects/Theme")]
public class Theme : ScriptableObject
{
    public Material Skybox;

    public Color TypingPanelColor;
    public Color NotificationPanelColor;
    public Color TextColor;
    public Color AccentColor;
    
    public KeyboardSE.Type KeyboardSE;
}
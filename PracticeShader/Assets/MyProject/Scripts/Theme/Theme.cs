using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "ScriptableObjects/Theme")]
public class Theme : ScriptableObject
{
    public Material Skybox;

    public Color TypingPanelColor = Color.black;
    public Color NotificationPanelColor = Color.black;
    public Color TextColor = Color.black;
    public Color AccentColorLight = Color.black;
    public Color AccentColorDark = Color.black;

    public KeyboardSE.Type KeyboardSE;
}
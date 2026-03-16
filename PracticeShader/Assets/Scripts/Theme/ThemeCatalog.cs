using UnityEngine;

[CreateAssetMenu(fileName = "ThemeCatalog", menuName = "ScriptableObjects/ThemeCatalog")]
public class ThemeCatalog : ScriptableObject
{
    public enum Type
    {
        SettingSun,
        Overcast,
        DarkStorm
    }

    [SerializeField] private Theme sunsetTheme;
    [SerializeField] private Theme overcastTheme;
    [SerializeField] private Theme darkStormTheme;
    
    public Theme GetTheme(ThemeCatalog.Type type)
    {
        return type switch
        {
            ThemeCatalog.Type.SettingSun => sunsetTheme,
            ThemeCatalog.Type.Overcast => overcastTheme,
            ThemeCatalog.Type.DarkStorm => darkStormTheme,
            _ => throw new System.ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
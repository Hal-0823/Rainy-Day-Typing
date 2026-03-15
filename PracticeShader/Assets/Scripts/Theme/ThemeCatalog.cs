using UnityEngine;

[CreateAssetMenu(fileName = "ThemeCatalog", menuName = "ScriptableObjects/ThemeCatalog")]
public class ThemeCatalog : ScriptableObject
{
    public enum Type
    {
        SettingSun,
        HeavyOvercast
    }

    [SerializeField] private Theme sunsetTheme;
    [SerializeField] private Theme overcastTheme;

    public Theme GetTheme(ThemeCatalog.Type type)
    {
        return type switch
        {
            ThemeCatalog.Type.SettingSun => sunsetTheme,
            ThemeCatalog.Type.HeavyOvercast => overcastTheme,
            _ => throw new System.ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
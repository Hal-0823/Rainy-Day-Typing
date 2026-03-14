using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "ScriptableObjects/Theme")]
public class Theme : ScriptableObject
{
    public enum Type
    {
        SettingSun,
        HeavyOvercast
    }

    [SerializeField] private Material _settingSunSky;
    [SerializeField] private Material _heavyOvercastSky;

    public Material GetSkybox(Type type)
    {
        switch (type)
        {
            case Type.SettingSun:
                return _settingSunSky;
            case Type.HeavyOvercast:
                return _heavyOvercastSky;
        }

        return null;
    }
}
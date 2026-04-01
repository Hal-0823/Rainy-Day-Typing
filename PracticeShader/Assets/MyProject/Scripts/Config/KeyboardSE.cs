using UnityEngine;

/// <summary>
/// キーボードの打鍵音を管理するクラス
/// </summary>
[CreateAssetMenu(fileName = "KeyboardSE", menuName = "SE/KeyboardSE", order = 1)]
public class KeyboardSE : ScriptableObject
{
    public enum Type
    {
        Mechanical,
        Typewriter,
        Light,
        None
    }

    [SerializeField]
    private AudioClip[] mechanicalSE;

    [SerializeField]
    private AudioClip[] typewriterSE;

    [SerializeField]
    private AudioClip[] lightSE;

    private Type _selectedType = Type.Mechanical;
    
    public void SetSelectedType(Type type)
    {
        _selectedType = type;
    }

    public int GetKeySECount()
    {
        switch (_selectedType)
        {
            case Type.Mechanical:
                return mechanicalSE.Length;
            case Type.Typewriter:
                return typewriterSE.Length;
            case Type.Light:
                return lightSE.Length;
            default:
                return 0;
        }
    }

    public AudioClip GetAudioClip(int index)
    {
        switch (_selectedType)
        {
            case Type.Mechanical:
                return mechanicalSE[index];
            case Type.Typewriter:
                return typewriterSE[index];
            case Type.Light:
                return lightSE[index];
            default:
                return null;
        }
    }
}
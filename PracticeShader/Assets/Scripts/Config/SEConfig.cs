using UnityEngine;
using GameConfig;

/// <summary>
/// 効果音設定を管理するScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SEConfig", menuName = "ScriptableObjects/SEConfig", order = 1)]
public class SEConfig : ScriptableObject
{
    [SerializeField]
    private AudioClip[] mechanicalSE;

    [SerializeField]
    private AudioClip[] typewriterSE;

    [SerializeField]
    private AudioClip[] lightSE;

    public KeySE SelectedKeySE;

    public int GetKeySECount()
    {
        switch (SelectedKeySE)
        {
            case KeySE.Mechanical:
                return mechanicalSE.Length;
            case KeySE.Typewriter:
                return typewriterSE.Length;
            case KeySE.Light:
                return lightSE.Length;
            default:
                return 0;
        }
    }

    public AudioClip GetAudioClip(int index)
    {
        switch (SelectedKeySE)
        {
            case KeySE.Mechanical:
                return mechanicalSE[index];
            case KeySE.Typewriter:
                return typewriterSE[index];
            case KeySE.Light:
                return lightSE[index];
            default:
                return null;
        }
    }
}
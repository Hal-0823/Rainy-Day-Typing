using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicData", order = 1)]
public class MusicData : ScriptableObject
{
    [SerializeField] private string _path;
    [SerializeField] private string _title;
    [SerializeField] private string _composer;

    public string Path => _path;
    public string Title => _title;
    public string Composer => _composer;
}
using System.Collections.Generic;
using R3;
using Utils;

public class MusicModel
{
    private ReactiveProperty<MusicData> _currentMusicData = new ReactiveProperty<MusicData>();
    public ReadOnlyReactiveProperty<MusicData> CurrentMusicData => _currentMusicData;

    private List<MusicData> _musicDataList;
    private int _currentIndex = -99;

    public MusicModel(List<MusicData> musicDataList)
    {
        _musicDataList = musicDataList;
    }

    public void RequestNextMusic()
    {
        _currentIndex++;
        if (_currentIndex >= _musicDataList.Count || _currentIndex < 0)
        {
            _musicDataList.Shuffle();
            _currentIndex = 0;
        }
        _currentMusicData.Value = _musicDataList[_currentIndex];
    }
}
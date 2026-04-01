using System.Collections.Generic;
using R3;
using Utils;

public class MusicModel
{
    private ReactiveProperty<MusicData> _currentMusicData = new();
    public ReadOnlyReactiveProperty<MusicData> CurrentMusicData => _currentMusicData;

    private ReactiveProperty<bool> _isPaused = new(true);
    public ReadOnlyReactiveProperty<bool> IsPaused => _isPaused;

    private ReactiveProperty<bool> _isPlaying = new(false);
    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    private List<MusicData> _musicDataList;
    private int _currentIndex = -99;

    public MusicModel(List<MusicData> musicDataList)
    {
        _musicDataList = musicDataList;
    }

    public void TogglePauseState()
    {
        _isPaused.Value = !_isPaused.Value;
    }

    // TODO : unpause中に呼ばれると再生されない
    public void SetPauseState(bool isPaused)
    {
        _isPaused.Value = isPaused;
    }

    public void SetPlayState(bool isPlaying)
    {
        _isPlaying.Value = isPlaying;
    }

    public void RequestPreviousMusic()
    {
        _currentIndex--;
        if (_currentIndex >= _musicDataList.Count || _currentIndex < 0)
        {
            _musicDataList.Shuffle();
            _currentIndex = 0;
        }
        _currentMusicData.Value = _musicDataList[_currentIndex];
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
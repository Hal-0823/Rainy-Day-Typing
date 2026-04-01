using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

public class MusicPlayerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _musicNameText;
    [SerializeField] private TextMeshProUGUI _comporserNameText;
    [SerializeField] private TwoStateButton _playPauseButton;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;

    public Observable<Unit> OnPlayPauseButtonClicked => _playPauseButton.OnClick;
    public Observable<Unit> OnNextButtonClicked => _nextButton.OnClickAsObservable();
    public Observable<Unit> OnPreviousButtonClicked => _previousButton.OnClickAsObservable();

    public void SetMusicInfo(string musicName, string composerName)
    {
        _musicNameText.text = musicName;
        _comporserNameText.text = composerName;
    }

    public void SetPlayState(bool isPlaying)
    {
        _playPauseButton.SetState(!isPlaying);
    }
}
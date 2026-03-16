using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using R3;
using System;

public class MusicView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _musicInfoCanvasGroup;
    [SerializeField] private TextMeshProUGUI _musicTitleText;
    [SerializeField] private TextMeshProUGUI _composerText;

    private Subject<Unit> _onMusicEndSubject = new Subject<Unit>();
    public Observable<Unit> OnMusicEnd => _onMusicEndSubject;

    // 通知処理のキャンセル用
    private CancellationTokenSource _notificationCts;

    private CancellationTokenSource _playMusicCts;

    private void Awake()
    {
        _musicInfoCanvasGroup.alpha = 0;
    }

    /// <summary>
    /// 音楽をフェードインして再生する
    /// </summary>
    /// <param name="musicClip"></param>
    /// <param name="fadeDuration"></param>
    /// <returns></returns>
    public async UniTask PlayWithFade(AudioClip musicClip, float fadeDuration)
    {
        _playMusicCts?.Cancel();
        _playMusicCts?.Dispose();

        _playMusicCts = new CancellationTokenSource();
        var token = _playMusicCts.Token;

        // 曲再生遅延
        await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);

        await AudioManager.Instance.MusicAudioController.PlayWithFade(musicClip, fadeDuration, token);
        WaitUntilMusicEnd(token).Forget();
    }

    private async UniTaskVoid WaitUntilMusicEnd(CancellationToken token)
    {
        await AudioManager.Instance.MusicAudioController.WaitUntilMusicEnd(token);
        if (!token.IsCancellationRequested)
        {
            _onMusicEndSubject.OnNext(Unit.Default);
        }
    }

    /// <summary>
    /// 音楽情報をフェードインして表示し、一定時間後にフェードアウトする
    /// </summary>
    /// <param name="musicTitle"></param>
    /// <param name="composer"></param>
    /// <param name="fadeDuration"></param>
    /// <param name="displayDuration"></param>
    /// <returns></returns>
    public async UniTaskVoid AnimateNotification(string musicTitle, string composer, float fadeDuration, float displayDuration)
    {
        if (_notificationCts != null)
        {
            _notificationCts.Cancel();
            _notificationCts.Dispose();
        }

        _notificationCts = new CancellationTokenSource();
        var token = _notificationCts.Token;

        _musicInfoCanvasGroup.DOKill();

        try
        {
            _musicTitleText.text = musicTitle;
            _composerText.text = "by " + composer;
            _musicInfoCanvasGroup.alpha = 0;
            await _musicInfoCanvasGroup.DOFade(1, fadeDuration).ToUniTask(cancellationToken: token);
            await UniTask.Delay(TimeSpan.FromSeconds(displayDuration), cancellationToken: token);
            await _musicInfoCanvasGroup.DOFade(0, fadeDuration).ToUniTask(cancellationToken: token);
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    private void OnDestroy()
    {
        _onMusicEndSubject.Dispose();
        _notificationCts?.Cancel();
        _notificationCts?.Dispose();
        _playMusicCts?.Cancel();
        _playMusicCts?.Dispose();
    }
}
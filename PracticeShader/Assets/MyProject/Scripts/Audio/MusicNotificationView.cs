using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using R3;
using System;

public class MusicNotificationView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _musicNotificationCanvasGroup;
    [SerializeField] private TextMeshProUGUI _musicTitleText;
    [SerializeField] private TextMeshProUGUI _composerText;

    // 通知処理のキャンセル用
    private CancellationTokenSource _notificationCts;

    private void Awake()
    {
        _musicNotificationCanvasGroup.alpha = 0;
    }

    public void StandbyNotification(string musicTitle, string composer)
    {
        _musicNotificationCanvasGroup.DOKill();
        _musicNotificationCanvasGroup.alpha = 0;
        _musicTitleText.text = musicTitle;
        _composerText.text = "by " + composer;
    }

    public async UniTaskVoid ShowNotification(float delay, float fadeDuration, float displayDuration)
    {
        if (_notificationCts != null)
        {
            _notificationCts.Cancel();
            _notificationCts.Dispose();
        }

        _notificationCts = new CancellationTokenSource();
        var token = _notificationCts.Token;
        //_musicNotificationCanvasGroup.DOKill();

        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            //_musicNotificationCanvasGroup.alpha = 0;
            await _musicNotificationCanvasGroup.DOFade(1, fadeDuration).ToUniTask(cancellationToken: token);
            await UniTask.Delay(TimeSpan.FromSeconds(displayDuration), cancellationToken: token);
            await _musicNotificationCanvasGroup.DOFade(0, fadeDuration).ToUniTask(cancellationToken: token);
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    private void OnDestroy()
    {
        _notificationCts?.Cancel();
        _notificationCts?.Dispose();
    }
}
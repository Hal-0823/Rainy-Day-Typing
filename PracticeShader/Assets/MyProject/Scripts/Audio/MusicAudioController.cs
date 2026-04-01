using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using R3;
using System;

public class MusicAudioController : MonoBehaviour
{
    private Subject<Unit> _onMusicEndSubject = new Subject<Unit>();
    public Observable<Unit> OnMusicEnd => _onMusicEndSubject;

    private AudioSourceWrapper _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSourceWrapper>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourceWrapperコンポーネントが見つかりません。", this);
        }
    }

    public void SetMusic(AudioClip musicClip)
    {
        _audioSource.Clip = musicClip;
    }

    public async UniTask PlayWithFade(float fadeDuration, CancellationToken token)
    {
        _audioSource.Volume = 0;
        PlayMusic();
        _audioSource.Play();
        await _audioSource.DOFade(1, fadeDuration).ToUniTask(cancellationToken: token);
        WaitUntilMusicEnd(token).Forget();
    }

    public async UniTaskVoid WaitUntilMusicEnd(CancellationToken token)
    {
        await _audioSource.WaitForMusicEnd(token);
        _onMusicEndSubject.OnNext(Unit.Default);
    }

    public void PauseMusic()
    {
        if (_audioSource.IsPaused) return;
        _audioSource.Pause();
    }

    public void PlayMusic()
    {
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}
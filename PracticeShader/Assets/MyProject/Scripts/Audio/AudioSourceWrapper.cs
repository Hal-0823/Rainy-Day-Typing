using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

/// <summary>
/// PauseのStateに対応したAudioSourceのラッパークラス
/// </summary>
public class AudioSourceWrapper : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    public bool IsPlaying => _audioSource.isPlaying;

    public float Volume
    {
        get => _audioSource.volume;
        set => _audioSource.volume = value;
    }

    public float Pitch
    {
        get => _audioSource.pitch;
        set => _audioSource.pitch = value;
    }

    public bool Loop
    {
        get => _audioSource.loop;
        set => _audioSource.loop = value;
    }

    public AudioClip Clip
    {
        get => _audioSource.clip;
        set => _audioSource.clip = value;
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントが見つかりません。", this);
        }
    }

    public void Pause()
    {
        _audioSource.Pause();
        _isPaused = true;
    }

    public void UnPause()
    {
        _audioSource.UnPause();
        _isPaused = false;
    }

    public void Play()
    {
        if (!IsPlaying)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.UnPause();
        }
        _isPaused = false; 
    }

    public void Stop()
    {
        _audioSource.Stop();
        _isPaused = false;
    }

    public async UniTask WaitForMusicEnd(CancellationToken token)
    {
        bool isFinished = false;

        try
        {
            while (!isFinished)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                if (!_audioSource.isPlaying && !_isPaused && _audioSource.time == 0)
                {
                    isFinished = true;
                }

                await UniTask.Yield();
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("曲の再生が中断されました。");
            throw;
        }
    }

#region DOTween Extension
    public TweenerCore<float, float, FloatOptions> DOFade(float endValue, float duration) => _audioSource.DOFade(endValue, duration);
#endregion
}
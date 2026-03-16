using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

public class MusicAudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSourceコンポーネントが見つかりません。", this);
        }
    }

    public async UniTask PlayWithFade(AudioClip musicClip, float fadeDuration, CancellationToken token)
    {
        _audioSource.volume = 0;
        _audioSource.clip = musicClip;
        _audioSource.Play();
        await _audioSource.DOFade(1, fadeDuration).ToUniTask(cancellationToken: token);
    }

    public async UniTask WaitUntilMusicEnd(CancellationToken token)
    {
        await UniTask.WaitUntil(() => !_audioSource.isPlaying, cancellationToken: token);
        _audioSource.Stop();    // 中断処理でisPlayingがfalseになるため、確実に停止させるためにStop()も呼ぶ
    }
}
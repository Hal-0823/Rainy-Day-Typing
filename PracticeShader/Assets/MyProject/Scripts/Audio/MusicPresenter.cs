using System;
using R3;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

public class MusicPresenter : IDisposable
{
    private const float FadeDuration = 1.0f;

    private readonly MusicModel _model;
    private readonly MusicAudioController _view;

    private readonly CompositeDisposable _disposables = new();
    private CancellationTokenSource _cancellationTokenSource = new();

    private UniTask _musicLoadTask;

    public MusicPresenter(MusicModel model)
    {
        _model = model;
        // 本来なら直接viewを参照するべきだが、シングルトンなAudioManagerを介して参照する
        _view = AudioManager.Instance.MusicAudioController;
        Bind();
    }

    private void Bind()
    {
        // TODO : 再生とポーズの関係を整理する
        // PlayerのPlay/Pauseボタンについて
        _model.IsPlaying
            .Subscribe(isPlaying => {
                if (isPlaying)
                {
                    PlayMusicAsync(FadeDuration).Forget();
                }
                else
                {
                    _view.Stop();
                }
            })
            .AddTo(_disposables);

        _model.IsPaused
            .Subscribe(isPaused => {
                if (isPaused)
                {
                    _view.PauseMusic();
                }
                else
                {
                    _view.PlayMusic();
                }
            })
            .AddTo(_disposables);

        _model.CurrentMusicData
            .Where(data => data != null)
            .Subscribe(async data => {
                LoadMusic(data);
            })
            .AddTo(_disposables);

        _view.OnMusicEnd
            .Subscribe(_ => _model.RequestNextMusic())
            .AddTo(_disposables);
    }

    private void LoadMusic(MusicData data)
    {
        _musicLoadTask = LoadMusicAsync(data).Preserve();
    }

    private async UniTask LoadMusicAsync(MusicData data)
    {
        var token = _cancellationTokenSource.Token;
        var musicLoader = new MusicLoader();
        var musicClip = await musicLoader.LoadMusicAsync(data.Path, token);
        _view.SetMusic(musicClip);
    }

    private async UniTask PlayMusicAsync(float fadeDuration)
    {
        await _musicLoadTask; // 音楽のロードが完了するのを待つ
        var token = _cancellationTokenSource.Token;
        _view.PlayWithFade(fadeDuration, token).Forget();
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
using System;
using R3;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MusicPresenter : IDisposable
{
    private readonly MusicModel _model;
    private readonly MusicView _view;

    private readonly CompositeDisposable _disposables = new();
    private CancellationTokenSource _cancellationTokenSource = new();

    public MusicPresenter(MusicModel model, MusicView view)
    {
        _model = model;
        _view = view;
        Bind();
        // 最初の曲をリクエスト
        _model.RequestNextMusic();
    }

    private void Bind()
    {
        _model.CurrentMusicData
            .Where(data => data != null)
            .Subscribe(async data =>
            {
                var token = _cancellationTokenSource.Token;
                var musicLoader = new MusicLoader();
                var audioClip = await musicLoader.LoadMusicAsync(data.Path);
                await _view.PlayWithFade(audioClip, 1f, token);
                _view.AnimateNotification(data.Title, data.Composer, 1.5f, 5f).Forget();
            })
            .AddTo(_disposables);

        _view.OnMusicEnd
            .Subscribe(_ => _model.RequestNextMusic())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
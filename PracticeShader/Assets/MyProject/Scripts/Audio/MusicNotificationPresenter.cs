using System;
using R3;
using Cysharp.Threading.Tasks;
using System.Threading;

public class MusicNotificationPresenter : IDisposable
{
    private readonly MusicModel _model;
    private readonly MusicNotificationView _view;

    private readonly CompositeDisposable _disposables = new();
    private CancellationTokenSource _cancellationTokenSource = new();

    public MusicNotificationPresenter(MusicModel model, MusicNotificationView view)
    {
        _model = model;
        _view = view;
        Bind();
    }

    private void Bind()
    {
        _model.CurrentMusicData
            .Where(data => data != null)
            .Subscribe(async data => _view.StandbyNotification(data.Title, data.Composer))
            .AddTo(_disposables);

        _model.IsPlaying
            .Where(isPlaying => isPlaying) // 再生開始時に通知を表示
            .Subscribe(_ => _view.ShowNotification(delay: 1f, fadeDuration: 1f, displayDuration: 3f).Forget())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}
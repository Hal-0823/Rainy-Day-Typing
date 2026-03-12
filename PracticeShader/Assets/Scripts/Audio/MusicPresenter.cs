using UnityEngine;
using R3;
using Cysharp.Threading.Tasks;

public class MusicPresenter
{
    private readonly MusicModel _model;
    private readonly MusicView _view;

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
                var musicLoader = new MusicLoader();
                var audioClip = await musicLoader.LoadMusicAsync(data.Path);
                await _view.PlayWithFade(audioClip, 1f);
                _view.AnimateNotification(data.Title, data.Composer, 1.5f, 5f).Forget();
            })
            .AddTo(_view);

        _view.OnMusicEnd
            .Subscribe(_ => _model.RequestNextMusic())
            .AddTo(_view);
    }
}
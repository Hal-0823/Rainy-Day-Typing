using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using System;
using ObservableCollections;

public class MusicPlayerPresenter : IDisposable
{
    private readonly MusicModel _model;
    private readonly MusicPlayerView _view;

    private readonly CompositeDisposable _disposables = new();

    public MusicPlayerPresenter(MusicModel model, MusicPlayerView view)
    {
        _model = model;
        _view = view;
        Bind();
    }

    private void Bind()
    {
        _model.CurrentMusicData
            .Where(data => data != null)
            .Subscribe(data => _view.SetMusicInfo(data.Title, data.Composer))
            .AddTo(_disposables);

        _model.IsPaused
            .Subscribe(isPaused => _view.SetPlayState(!isPaused))
            .AddTo(_disposables);

        // 次曲・前曲のリクエストと同時に再生状態を再生にする
        _view.OnNextButtonClicked
            .Subscribe(_ => {
                _model.RequestNextMusic();
                _model.SetPlayState(true);
            })
            .AddTo(_disposables);

        _view.OnPreviousButtonClicked
            .Subscribe(_ => {
                _model.RequestPreviousMusic();
                _model.SetPlayState(true);
            })
            .AddTo(_disposables);

        _view.OnPlayPauseButtonClicked
            .Subscribe(_ => _model.TogglePauseState())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
using R3;
using Cysharp.Threading.Tasks;
using System;

public class TypingPresenter : IDisposable
{
    private readonly TypingModel _model;
    private readonly TypingView _view;
    private readonly AudioManager _audioManager;

    private readonly CompositeDisposable _disposables = new();

    public TypingPresenter(TypingModel model, TypingView view, AudioManager audioManager)
    {
        _model = model;
        _view = view;
        _audioManager = audioManager;
        _model.NextWord();
        Bind();
    }

    private void Bind()
    {
        _model.CurrentState
            .Subscribe(state => _view.SetTypingText(state))
            .AddTo(_disposables);
        
        _model.OnWordCompleted
            .Subscribe(_ => _model.NextWord())
            .AddTo(_disposables);

        _view.OnInputChar
            .Subscribe(c => {
                _model.HandleInput(c);
                _audioManager.KeyboardAudioController.PlayRandomKeySE();
            })
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
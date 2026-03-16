using R3;
using Cysharp.Threading.Tasks;
using System;

public class TypingPresenter : IDisposable
{
    private readonly TypingModel _model;
    private readonly TypingView _view;

    private readonly CompositeDisposable _disposables = new();

    public TypingPresenter(TypingModel model, TypingView view)
    {
        _model = model;
        _view = view;
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
                AudioManager.Instance.KeyboardAudioController.PlayRandomKeySE();
            })
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
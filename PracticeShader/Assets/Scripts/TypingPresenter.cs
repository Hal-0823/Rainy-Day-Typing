using R3;
using Cysharp.Threading.Tasks;

public class TypingPresenter
{
    private readonly TypingModel _model;
    private readonly TypingView _view;

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
            .AddTo(_view);
        
        _model.OnWordCompleted
            .Subscribe(_ => _model.NextWord())
            .AddTo(_view);

        _view.OnInputChar
            .Subscribe(c => _model.HandleInput(c))
            .AddTo(_view);
    }
}
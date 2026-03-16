using System;
using System.Collections.Generic;
using R3;

public class SettingsPresenter : IDisposable
{
    private readonly SettingsModel _model;
    private readonly SettingsView _view;
    private readonly AudioManager _audioManager;
    private readonly RenderVolumeController _renderVolumeController;
    private readonly ThemeController _themeController;

    private readonly CompositeDisposable _disposables = new();

    public SettingsPresenter(SettingsModel model, SettingsView view, RenderVolumeController renderVolumeController, ThemeController themeController)
    {
        _model = model;
        _view = view;
        _renderVolumeController = renderVolumeController;
        _themeController = themeController;
        AudioBind();
        MenuBind();
        ThemeBind();
    }

    private void AudioBind()
    {
        _model.MusicVolume
            .Subscribe(volume =>
            {
                _view.SetMusicVolume(volume);
                AudioManager.Instance.SetMusicVolume(volume);
            })
            .AddTo(_disposables);

        _model.RainVolume
            .Subscribe(volume =>
            {
                _view.SetRainVolume(volume);
                AudioManager.Instance.SetRainVolume(volume);
            })
            .AddTo(_disposables);

        _model.KeyboardVolume
            .Subscribe(volume =>
            {
                // メニューが開いているときの音量変更は音を鳴らす
                _view.SetKeyboardVolume(volume, playSE: _model.IsMenuOpen.Value);
                AudioManager.Instance.SetKeyboardVolume(volume);
            })
            .AddTo(_disposables);

        _model.SelectedKeyboardType
            .Subscribe(type => AudioManager.Instance.SetKeyboardSEType(type))
            .AddTo(_disposables);
        
        _view.OnMusicVolumeChanged
            .Subscribe(volume => _model.MusicVolume.Value = volume)
            .AddTo(_disposables);
        
        _view.OnRainVolumeChanged
            .Subscribe(volume => _model.RainVolume.Value = volume)
            .AddTo(_disposables);

        _view.OnKeyboardVolumeChanged
            .Subscribe(volume => _model.KeyboardVolume.Value = volume)
            .AddTo(_disposables);
    }

    private void MenuBind()
    {
        _model.IsMenuOpen
            .Subscribe(isOpen => {
                _view.ChangeMenuVisibility(isOpen);
                _renderVolumeController.SetBlur(isOpen);
            })
            .AddTo(_disposables);

        _view.OnPressedTabButton
            .Subscribe(_ => _model.RequestToggleMenu())
            .AddTo(_disposables);
    }

    private void ThemeBind()
    {
        _model.SelectedTheme
            .Subscribe(theme => {
                _view.SetCurrentTheme(theme.ToString());
                _themeController.SetTheme(theme);
            })
            .AddTo(_disposables);
        
        _view.OnPressedLeftShiftThemeButton
            .Subscribe(_ => {
                _model.RequestShiftTheme(-1);
                AudioManager.Instance.SystemAudioController.PlayButtonClickSE();
            })
            .AddTo(_disposables);
        
        _view.OnPressedRightShiftThemeButton
            .Subscribe(_ => {
                _model.RequestShiftTheme(1);
                AudioManager.Instance.SystemAudioController.PlayButtonClickSE();
            })
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

public class TypingSceneDirector : MonoBehaviour, ISceneDirector
{
    [SerializeField] private TypingView _typingView;
    [SerializeField] private string _questDataPath = "Rain";

    [SerializeField] private MusicView _musicView;
    [SerializeField] private List<MusicData> _musicDataList;

    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private RenderVolumeController _renderVolumeController;
    [SerializeField] private ThemeController _themeController;

    private TypingPresenter _typingPresenter;
    private MusicPresenter _musicPresenter;
    private SettingsPresenter _settingsPresenter;

    public async UniTask OnLoadScene()
    {
        var questLoader = new QuestLoader();
        var quests = await questLoader.LoadQuestDataAsync(_questDataPath);

        var model = new TypingModel(quests);
        _typingPresenter = new TypingPresenter(model, _typingView);

        var musicModel = new MusicModel(_musicDataList);
        _musicPresenter = new MusicPresenter(musicModel, _musicView);

        var settingsModel = new SettingsModel();
        _settingsPresenter = new SettingsPresenter(settingsModel, _settingsView, _renderVolumeController, _themeController);

        AudioManager.Instance.RainAudioController.PlayRainSound();
    }

    public async UniTask OnUnloadScene()
    {
        _typingPresenter?.Dispose();
        _musicPresenter?.Dispose();
        _settingsPresenter?.Dispose();
        await UniTask.Yield();
    }
}
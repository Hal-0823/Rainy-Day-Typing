using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using System;

public class TypingSceneDirector : MonoBehaviour, ISceneDirector
{
    [SerializeField] private TypingView _typingView;
    [SerializeField] private string _questDataPath = "Rain";

    // ここに本来ならMusicViewに相当するものが入るが、シングルトンなAudioManagerを経由してviewを参照するため、直接参照しない
    // [SerializeField] private MusicView _musicView;
    [SerializeField] private MusicNotificationView _musicNotificationView;
    [SerializeField] private MusicPlayerView _musicPlayerView;
    
    [SerializeField] private List<MusicData> _musicDataList;

    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private RenderVolumeController _renderVolumeController;
    [SerializeField] private ThemeController _themeController;

    private TypingPresenter _typingPresenter;
    private MusicPresenter _musicPresenter;
    private MusicNotificationPresenter _musicNotificationPresenter;
    private MusicPlayerPresenter _musicPlayerPresenter;
    private SettingsPresenter _settingsPresenter;

    public async UniTask OnLoadScene()
    {
        var questLoader = new QuestLoader();
        var quests = await questLoader.LoadQuestDataAsync(_questDataPath);

        var model = new TypingModel(quests);
        _typingPresenter = new TypingPresenter(model, _typingView);

        var musicModel = new MusicModel(_musicDataList);
        _musicPresenter = new MusicPresenter(musicModel);
        _musicPlayerPresenter = new MusicPlayerPresenter(musicModel, _musicPlayerView);
        _musicNotificationPresenter = new MusicNotificationPresenter(musicModel, _musicNotificationView);

        var settingsModel = new SettingsModel();
        _settingsPresenter = new SettingsPresenter(settingsModel, _settingsView, _renderVolumeController, _themeController);

        AudioManager.Instance.RainAudioController.PlayRainSound();

        musicModel.RequestNextMusic(); // 最初の曲をセット

    }

    public async UniTask OnUnloadScene()
    {
        _typingPresenter?.Dispose();
        _musicPresenter?.Dispose();
        _musicPlayerPresenter?.Dispose();
        _musicNotificationPresenter?.Dispose();
        _settingsPresenter?.Dispose();
        await UniTask.Yield();
    }
}
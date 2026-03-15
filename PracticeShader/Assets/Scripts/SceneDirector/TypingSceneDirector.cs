using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

public class TypingSceneDirector : MonoBehaviour
{
    [SerializeField] private TypingView _typingView;
    [SerializeField] private string _questDataPath = "Rain";

    [SerializeField] private MusicView _musicView;
    [SerializeField] private List<MusicData> _musicDataList;

    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private RenderVolumeController _renderVolumeController;
    [SerializeField] private ThemeController _themeController;

    private async void Start()
    {
        var questLoader = new QuestLoader();
        var quests = await questLoader.LoadQuestDataAsync(_questDataPath);

        var model = new TypingModel(quests);
        var presenter = new TypingPresenter(model, _typingView, _audioManager);

        var musicModel = new MusicModel(_musicDataList);
        var musicPresenter = new MusicPresenter(musicModel, _musicView);

        var settingsModel = new SettingsModel();
        var settingsPresenter = new SettingsPresenter(settingsModel, _settingsView, _audioManager, _renderVolumeController, _themeController);
    }
}
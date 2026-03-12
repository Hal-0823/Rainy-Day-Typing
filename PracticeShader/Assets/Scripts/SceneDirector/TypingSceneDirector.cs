using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class TypingSceneDirector : MonoBehaviour
{
    [SerializeField] private TypingView _typingView;
    [SerializeField] private string _questDataPath = "Rain";

    [SerializeField] private MusicView _musicView;
    [SerializeField] private List<MusicData> _musicDataList;

    private async void Start()
    {
        var questLoader = new QuestLoader();
        var quests = await questLoader.LoadQuestDataAsync(_questDataPath);

        var model = new TypingModel(quests);
        var presenter = new TypingPresenter(model, _typingView);

        var musicModel = new MusicModel(_musicDataList);
        var musicPresenter = new MusicPresenter(musicModel, _musicView);
    }
}
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class IntroSceneDirector : MonoBehaviour, ISceneDirector
{
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _startButton.onClick.AddListener(HandleStartButtonClick);
        _startButton.interactable = true;
    }

    private void HandleStartButtonClick()
    {
        SceneNavigator.Insatnce.LoadSceneAsync("Game").Forget();
        AudioManager.Instance.SystemAudioController.PlaySceneTransitionSE();
        _startButton.interactable = false;
    }

    public async UniTask OnLoadScene()
    {
        await UniTask.Yield();
    }

    public async UniTask OnUnloadScene()
    {
        await UniTask.Yield();
    }
}
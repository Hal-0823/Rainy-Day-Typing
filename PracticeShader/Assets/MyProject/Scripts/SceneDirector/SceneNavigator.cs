using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;

public class SceneNavigator : MonoBehaviour
{
    public static SceneNavigator Insatnce { get; private set; }

    [SerializeField] private float _fadeInDuration = 1f;
    [SerializeField] private float _fadeOutDuration = 1f;
    [SerializeField] private float _fadeInDelay = 1f;
    [SerializeField] private float _fadeOutDelay = 1f;
    [SerializeField] private float _minimumLoadingTime = 1f;

    private CanvasGroup _fadeCanvasGroup;

    private void Awake()
    {
        if (Insatnce != null && Insatnce != this)
        {
            Destroy(gameObject);
            return;
        }
        Insatnce = this;
        DontDestroyOnLoad(gameObject);

        _fadeCanvasGroup = GetComponentInChildren<CanvasGroup>();
        if (_fadeCanvasGroup == null)
        {
            Debug.LogError("CanvasGroupが見つかりませんでした。",this);
            return;
        }

        Init();

        LoadSceneAsync(SceneManager.GetActiveScene().name).Forget();
    }

    private void Init()
    {
        _fadeCanvasGroup.alpha = 1f;
        _fadeCanvasGroup.blocksRaycasts = true;
    }

    public async UniTask LoadSceneAsync(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            var currentSceneComponents = FindObjectsByType<Component>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            ISceneDirector currentSceneDirector = currentSceneComponents.OfType<ISceneDirector>().FirstOrDefault();

            if (currentSceneDirector == null) 
            {
                Debug.LogError($"現在のシーンにISceneDirectorを実装したオブジェクトが見つかりませんでした。", this);
                return;
            }
            await FadeOut();
            await UniTask.WhenAll(
                currentSceneDirector.OnUnloadScene(),
                SceneManager.LoadSceneAsync(sceneName).ToUniTask(),
                UniTask.Delay(System.TimeSpan.FromSeconds(_minimumLoadingTime))
            );
        }

        var newSceneComponents = FindObjectsByType<Component>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        ISceneDirector newSceneDirector = newSceneComponents.OfType<ISceneDirector>().FirstOrDefault();
        
        if (newSceneDirector == null) 
        {
            Debug.LogError($"新しいシーンにISceneDirectorを実装したオブジェクトが見つかりませんでした。", this);
            return;
        }

        await newSceneDirector.OnLoadScene();
        FadeIn().Forget();
    }
    
    /// <summary>
    /// シーンに入るときのフェードイン処理
    /// </summary>
    /// <returns></returns>
    private async UniTask FadeIn()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(_fadeInDelay));
        await _fadeCanvasGroup.DOFade(0f, _fadeInDuration).ToUniTask();
        _fadeCanvasGroup.blocksRaycasts = false;

    }

    /// <summary>
    /// シーンから出るときのフェードアウト処理
    /// </summary>
    /// <returns></returns>
    private async UniTask FadeOut()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(_fadeOutDelay));
        _fadeCanvasGroup.blocksRaycasts = true;
        await _fadeCanvasGroup.DOFade(1f, _fadeOutDuration).ToUniTask();
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

public class TypingView : MonoBehaviour
{
    [Header("TypingText")]
    //[SerializeField] private TextMeshProUGUI fText;
    [SerializeField] private TextMeshProUGUI qText;
    [SerializeField] private TextMeshProUGUI rText;
    [SerializeField] private Color typedColor = Color.gray;
    [SerializeField] private Color remainColor = Color.white;

    [Header("Background")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color backgroundColor = Color.black;

    // 文字入力イベント
    private readonly Subject<char> _onInputCharSubject = new Subject<char>();
    public Observable<char> OnInputChar => _onInputCharSubject;

    private void Awake()
    {
        backgroundImage.color = backgroundColor;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                _onInputCharSubject.OnNext(c);
            }
        }
    }

    public void SetTypingText(TypingModel.State state)
    {
        //fText.text = state.FString;
        qText.text = state.QString;

        string typed = state.RString.Substring(0, state.RNum);
        string remain = state.RString.Substring(state.RNum);

        string typedHex = ColorUtility.ToHtmlStringRGB(typedColor);
        string remainHex = ColorUtility.ToHtmlStringRGB(remainColor);

        rText.text = $"<color=#{typedHex}>{typed}</color><color=#{remainHex}>{remain}</color>";
    }

    public void Clear()
    {
        //fText.text = "";
        qText.text = "";
        rText.text = "";
    }
}
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using R3;

/// <summary>
/// 2つの状態を持つボタンのコンポーネント
/// </summary>
public class TwoStateButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _state1Sprite;
    [SerializeField] private Sprite _state2Sprite;

    private bool _isState1 = true;
    public bool IsState1 => _isState1;

    public Observable<Unit> OnClick => _button.OnClickAsObservable().Select(_ => Unit.Default); 

    private void Start()
    {
        _button.onClick.AddListener(ToggleState);
        _button.image.sprite = _state1Sprite;
    }

    public void SetState(bool isState1)
    {
        _isState1 = isState1;
        _button.image.sprite = _isState1 ? _state1Sprite : _state2Sprite;
    }

    private void ToggleState()
    {
        _isState1 = !_isState1;
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        DOTween.Kill(_button.transform);
        _button.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
        _button.image.sprite = _isState1 ? _state1Sprite : _state2Sprite;
    }
}
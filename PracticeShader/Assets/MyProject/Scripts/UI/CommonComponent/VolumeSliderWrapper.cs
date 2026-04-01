using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

/// <summary>
/// ボリュームスライダーのUIを管理するクラス
/// スライダーの変更通知を提供する
/// </summary>
public class VolumeSliderWrapper : MonoBehaviour
{
    private Slider _slider;
    private TextMeshProUGUI _valueText;

    public Observable<int> OnVolumeChanged => _slider.OnValueChangedAsObservable().Select(value => (int)value);

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
        _valueText = GetComponentInChildren<TextMeshProUGUI>();

        if (_slider == null || _valueText == null)
        {
            Debug.LogError("ボリュームスライダーの構造に問題があります。", this);
        }
    }

    public void SetSliderValue(int value)
    {
        if (_slider.value != value) _slider.value = value;
        _valueText.text = value.ToString();
    }
}
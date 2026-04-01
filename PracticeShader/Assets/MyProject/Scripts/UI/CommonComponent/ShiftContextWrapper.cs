using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;

/// <summary>
/// コンテキストのシフト操作を管理するクラス
/// </summary>
public class ShiftContextWrapper : MonoBehaviour
{
    [SerializeField] private Button _leftShiftButton;
    [SerializeField] private Button _rightShiftButton;
    [SerializeField] private TextMeshProUGUI _currentContext;

    public Observable<Unit> OnPressedLeftShiftButton => _leftShiftButton.OnClickAsObservable();
    public Observable<Unit> OnPressedRightShiftButton => _rightShiftButton.OnClickAsObservable();

    public void SetCurrentContext(string context)
    {
        if (_currentContext == null) return;
        _currentContext.text = context;
    }
}
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIの閉じるボタンの挙動を制御するクラス
/// 閉じる対象のUIは_targetに指定する
/// </summary>
public class CloseButton : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _target.SetActive(false);
    }
}

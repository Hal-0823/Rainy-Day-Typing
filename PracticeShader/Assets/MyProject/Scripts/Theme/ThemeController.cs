using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class ThemeController : MonoBehaviour
{
    // テーマカラー適用対象リスト
    public static List<ThemeColorApplyBase> Targets = new();

    [SerializeField] private ThemeCatalog _themeCatalog;

    public void SetTheme(ThemeCatalog.Type type)
    {
        var theme = _themeCatalog.GetTheme(type);
        RenderSettings.skybox = theme.Skybox;

        // すべてのColorApplierにテーマカラーを適用
        foreach (var target in Targets)
        {
            target.ApplyThemeColor(theme);
        }

        AudioManager.Instance.SetKeyboardSEType(theme.KeyboardSE);
    }

}
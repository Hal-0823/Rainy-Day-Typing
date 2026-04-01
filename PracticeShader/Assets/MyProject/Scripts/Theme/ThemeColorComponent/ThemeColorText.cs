using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ThemeColorText : ThemeColorApplyBase
{   
    public override void ApplyThemeColor(Theme theme)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = theme.TextColor;
        }
    }
}

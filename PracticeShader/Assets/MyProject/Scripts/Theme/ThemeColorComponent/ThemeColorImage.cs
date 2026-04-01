using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ThemeColorImage : ThemeColorApplyBase
{
    private enum Type
    {
        TypingPanel,
        NotificationPanel,
        AccentLight,
        AccentDark
    }

    [SerializeField] private Type _type;

    public override void ApplyThemeColor(Theme theme)
    {
        var image = GetComponent<Image>();
        if (image != null)
        {
            switch (_type)
            {
                case Type.TypingPanel:
                    image.color = theme.TypingPanelColor;
                    break;
                case Type.NotificationPanel:
                    image.color = theme.NotificationPanelColor;
                    break;
                case Type.AccentLight:
                    image.color = theme.AccentColorLight;
                    break;
                case Type.AccentDark:
                    image.color = theme.AccentColorDark;
                    break;
            }
        }
    }
}

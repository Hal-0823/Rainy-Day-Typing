using UnityEngine;

public abstract class ThemeColorApplyBase : MonoBehaviour
{
    protected virtual void Awake()
    {
        ThemeController.Targets.Add(this);
    }

    protected virtual void OnDestroy()
    {
        ThemeController.Targets.Remove(this);
    }

    public abstract void ApplyThemeColor(Theme theme);
}
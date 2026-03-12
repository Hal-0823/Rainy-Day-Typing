using UnityEngine;
using TMPro;
using DG.Tweening;

public class DarkText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();

        DOVirtual.Float(0.0f, 1.0f, 4.0f, (value) =>
        {
            textMesh.fontSharedMaterial.SetFloat("_OutlineWidth", value);
        }).SetDelay(1.0f);
    }

    // フォントの変化をもとに戻す
    void OnApplicationQuit()
    {
        textMesh.fontSharedMaterial.SetFloat("_OutlineWidth", 0.0f);
    }
}



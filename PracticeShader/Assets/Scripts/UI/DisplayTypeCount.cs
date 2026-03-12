using UnityEngine;
using TMPro;

public class DisplayTypeCount : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI typeCountText;

    private void OnEnable()
    {
        GameManager.OnTypeCountUpdated += UpdateTypeCountDisplay;
    }

    private void OnDisable()
    {
        GameManager.OnTypeCountUpdated -= UpdateTypeCountDisplay;
    }

    private void UpdateTypeCountDisplay(int typeCount)
    {
        if (typeCountText != null)
        {
            typeCountText.text = "T:" + typeCount.ToString("d6");
        }
    }
}
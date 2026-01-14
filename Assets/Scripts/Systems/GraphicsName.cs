using TMPro;
using UnityEngine;

public class GraphicsName : MonoBehaviour
{
    private TextMeshProUGUI presetName;
    void Start()
    {
        presetName = GetComponentInChildren<TextMeshProUGUI>();
        presetName.text = $"{QualitySettings.names[QualitySettings.GetQualityLevel()]} (Default)";
    }
}

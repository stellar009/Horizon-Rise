using TMPro;
using UnityEngine;

public class GraphicsAutoConfigSystem : MonoBehaviour
{
    public TextMeshProUGUI autoFPSText, graphicMemoryText, graphicQualityText;
    int maxGraphicMemory;

    private void Start()
    {
        maxGraphicMemory = SystemInfo.graphicsMemorySize;
        DetectHardwareAndOptimize();
    }

    void DetectHardwareAndOptimize()
    {
        graphicMemoryText.text = $"Graphic Memory: {maxGraphicMemory}MB";

        if (SystemInfo.graphicsMemorySize > 4096)
        {
            Application.targetFrameRate = 60;
            autoFPSText.text = $"Max Allowed Frame Rate: {Application.targetFrameRate}";

            QualitySettings.SetQualityLevel(4);
            graphicQualityText.text = $"Current Graphics: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";

            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
            QualitySettings.antiAliasing = 4;
        }
        else if(SystemInfo.graphicsMemorySize > 2048)
        {
            Application.targetFrameRate = 45;
            autoFPSText.text = $"Max Allowed Frame Rate: {Application.targetFrameRate}";

            QualitySettings.SetQualityLevel(3);
            graphicQualityText.text = $"Current Graphics: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";

            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
            QualitySettings.antiAliasing = 2;
        }
        else if(SystemInfo.graphicsMemorySize > 1024)
        {
            Application.targetFrameRate = 30;
            autoFPSText.text = $"Max Allowed Frame Rate: {Application.targetFrameRate}";

            QualitySettings.SetQualityLevel(2);
            graphicQualityText.text = $"Current Graphics: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";

            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.antiAliasing = 0;
        }
        else if (SystemInfo.graphicsMemorySize > 512)
        {
            Application.targetFrameRate = 30;
            autoFPSText.text = $"Max Allowed Frame Rate: {Application.targetFrameRate}";

            QualitySettings.SetQualityLevel(1);
            graphicQualityText.text = $"Current Graphics: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";

            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.antiAliasing = 0;
        }
        else
        {
            Application.targetFrameRate = 30;
            autoFPSText.text = $"Max Allowed Frame Rate: {Application.targetFrameRate}";

            QualitySettings.SetQualityLevel(0);
            graphicQualityText.text = $"Current Graphics: {QualitySettings.names[QualitySettings.GetQualityLevel()]}";

            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            QualitySettings.antiAliasing = 0;
        }
    }
}

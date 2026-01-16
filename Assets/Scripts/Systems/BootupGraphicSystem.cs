using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class BootupGraphicSystem : MonoBehaviour
{
    int virtualRamSize;
    string gpuName;
    Camera mainCamera;
    UniversalAdditionalCameraData mainCameraData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        virtualRamSize = SystemInfo.graphicsMemorySize;
        gpuName = SystemInfo.graphicsDeviceName;
        gpuName = gpuName.ToLower();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySettings();
    }

    private void ApplySettings()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCameraData = mainCamera.GetUniversalAdditionalCameraData();
            GraphicsSetup(gpuName);
        }
    }

    void GraphicsSetup(string gpuName)
    {
        mainCameraData.antialiasing = AntialiasingMode.None;
        mainCameraData.renderPostProcessing = true;

        if (gpuName.Contains("intel")) //Lowest
        {
            SetLowestGraphics();
        }
        else if (gpuName.Contains("nvidia"))
        {
            if (virtualRamSize > 4096) //Highest
            {
                SetHighestGraphics();
            }
            else if (virtualRamSize > 2048) //High
            {
                SetHighGraphics();
            }
            else if (virtualRamSize > 1024) //Medium
            {
                SetMediumGraphics();
            }
            else //Low
            {
                SetLowGraphics();
            }
        }
        else //Fallback and AMD graphics
        {
            SetMediumGraphics();
        }
    }

    void SetHighestGraphics()
    {
        Application.targetFrameRate = 60;
        QualitySettings.SetQualityLevel(4);
        QualitySettings.antiAliasing = 4;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        EnableEmission();
    }

    void SetHighGraphics()
    {
        Application.targetFrameRate = 45;
        QualitySettings.SetQualityLevel(3);
        QualitySettings.antiAliasing = 2;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        EnableEmission();
    }

    void SetMediumGraphics()
    {
        Application.targetFrameRate = 30;
        QualitySettings.SetQualityLevel(2);
        QualitySettings.antiAliasing = 0;
        mainCameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
        mainCameraData.antialiasingQuality = AntialiasingQuality.High;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        EnableEmission();
    }

    void SetLowGraphics()
    {
        Application.targetFrameRate = 30;
        QualitySettings.SetQualityLevel(1);
        QualitySettings.antiAliasing = 0;
        mainCameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
        mainCameraData.antialiasingQuality = AntialiasingQuality.Low;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        DisableEmission();
    }

    void SetLowestGraphics()
    {
        Application.targetFrameRate = 24;
        QualitySettings.SetQualityLevel(0);
        mainCameraData.antialiasing = AntialiasingMode.None;
        mainCameraData.renderPostProcessing = false;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        DisableEmission();
    }

    void EnableEmission()
    {
        Shader.EnableKeyword("_EMISSION");
    }

    void DisableEmission()
    {
        Shader.DisableKeyword("_EMISSION");
    }
}
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
        if (gpuName.Contains("intel")) //Lowest
        {
            SetLowestGraphics();
            DisableEmission();
        }
        else if (gpuName.Contains("nvidia"))
        {
            mainCameraData.antialiasing = AntialiasingMode.None;
            mainCameraData.renderPostProcessing = true;

            if (virtualRamSize > 4096) //Highest
            {
                SetHighestGraphics();
                EnableEmission();
            }
            else if (virtualRamSize > 2048) //High
            {
                SetHighGraphics();
                EnableEmission();
            }
            else if (virtualRamSize > 1024) //Medium
            {
                SetMediumGraphics();
                EnableEmission();
            }
            else //Low
            {
                SetLowGraphics();
                DisableEmission();
            }
        }
        else //Fallback and AMD graphics
        {
            SetLowGraphics();
            DisableEmission();
        }
    }

    void SetHighestGraphics()
    {
        Application.targetFrameRate = 60;
        QualitySettings.SetQualityLevel(4);
        QualitySettings.antiAliasing = 4;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
    }

    void SetHighGraphics()
    {
        Application.targetFrameRate = 45;
        QualitySettings.SetQualityLevel(3);
        QualitySettings.antiAliasing = 2;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
    }

    void SetMediumGraphics()
    {
        Application.targetFrameRate = 30;
        QualitySettings.SetQualityLevel(2);
        QualitySettings.antiAliasing = 0;
        mainCameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
        mainCameraData.antialiasingQuality = AntialiasingQuality.High;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    }

    void SetLowGraphics()
    {
        Application.targetFrameRate = 30;
        QualitySettings.SetQualityLevel(1);
        QualitySettings.antiAliasing = 0;
        mainCameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
        mainCameraData.antialiasingQuality = AntialiasingQuality.Low;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
    }

    void SetLowestGraphics()
    {
        Application.targetFrameRate = 24;
        QualitySettings.SetQualityLevel(0);
        mainCameraData.antialiasing = AntialiasingMode.None;
        mainCameraData.renderPostProcessing = false;
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
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
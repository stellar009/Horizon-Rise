using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerLockPuzzle : MonoBehaviour
{
    [Header("Password Settings")]
    public string correctPassword = "1234";
    private int maxPasswordLength = 4;
    public ServerLockTrigger trigger;

    [Header("UI Settings")]
    public TextMeshProUGUI passwordDisplay;
    public GameObject lockCanva;
    public Button[] buttons;

    [Header("SFX Settings")]
    public AudioClip buttonPressSFX;
    public AudioClip wrongPassSFX;

    [Header("Scene Settings")]
    public GameObject loadingPanel;

    private StringBuilder passwordInput = new StringBuilder();
    private bool isLocked = true;

    private Vector3 originalCanvasPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        ResetDisplay();
        loadingPanel.SetActive(false);

        if (lockCanva != null)
        {
            originalCanvasPosition = lockCanva.transform.position;
        }
    }

    public void AddDigit(string digit)
    {
        if (!isLocked) return;
        if (passwordInput.Length >= maxPasswordLength) return;

        audioSource.PlayOneShot(buttonPressSFX);
        passwordInput.Append(digit);
        UpdateDisplay();

        if (passwordInput.Length >= maxPasswordLength)
        {
            SubmitPassword();
        }
    }

    public void ClearInput()
    {
        if (!isLocked) return;
        passwordInput.Clear();
        UpdateDisplay();
    }

    public void SubmitPassword()
    {
        if (!isLocked) return;

        if (passwordInput.ToString() == correctPassword)
        {
            SetButtonInteractables(false);
            UnlockDoor();
            trigger.enabled = false;
            Invoke(nameof(CloseInterface), 0.5f);
        }
        else
        {
            WrongPassword();
            Invoke(nameof(ResetDisplay), 1f);
        }
    }

    private void UnlockDoor()
    {
        isLocked = false;
        GameManager.instance.ActivateCursor(false);
        passwordDisplay.color = Color.green;
        StartCoroutine(LoadSceneCore());
    }

    private void WrongPassword()
    {
        audioSource.PlayOneShot(wrongPassSFX);
        passwordDisplay.color = Color.red;
        Invoke(nameof(ClearInput), 1f);
    }

    private void ResetDisplay()
    {
        string placeholder = "";
        for (int i = 0; i < maxPasswordLength; i++)
        {
            placeholder += "-";
        }
        passwordDisplay.text = placeholder;
        passwordDisplay.color = Color.gray;
    }

    private void SetButtonInteractables(bool interact)
    {
        foreach (Button button in buttons)
            button.interactable = interact;
    }

    public void OpenInterface()
    {
        lockCanva.SetActive(true);
    }

    public void CloseInterface()
    {
        passwordInput.Clear();
        ResetDisplay();
        GameManager.instance.ActivateCursor(false);
        lockCanva.SetActive(false);
    }

    void UpdateDisplay()
    {
        string display = "";
        for (int i = 0; i < passwordInput.Length; i++)
        {
            display += "*";
        }
        for (int i = passwordInput.Length; i < maxPasswordLength; i++)
        {
            display += "-";
        }

        passwordDisplay.text = display;
        passwordDisplay.color = Color.white;
    }

    IEnumerator LoadSceneCore()
    {
        loadingPanel.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync("Core");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if(operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                loadingPanel.SetActive(false);
            }
            yield return null;
        }
    }
}
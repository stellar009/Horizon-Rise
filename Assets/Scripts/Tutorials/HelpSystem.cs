using TMPro;
using UnityEngine;

public class HelpSystem : MonoBehaviour
{
    private AudioSource audioSource;
    float timer;

    public StoryManager storyManager;

    [Header("Help Menu Settings")]
    public GameObject helpMenu;
    public TextMeshProUGUI helpText;
    [Tooltip("The sequence is started from Element 0 which shows Binary Table and so on")]
    public string[] messages;

    [Header("SFX Settings")]
    public AudioClip SlideSFX;

    [Header("Animation Settings")]
    public Animator helpMenuAnimator;
    public string openAnimationName = "Open";
    public string closeAnimationName = "Close";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        helpMenu.SetActive(false);
        if (!storyManager)
        {
            GameManager.instance.playerInputActions.Player.HelpMenu.Disable();
        }
    }

    public void OpenHelpMenu()
    {
        audioSource.PlayOneShot(SlideSFX);
        helpMenuAnimator.Play(openAnimationName);
        GameManager.instance.ActivateCursor(true);
        storyManager.DIsableUIElements();
        helpMenu.SetActive(true);
        ShowBinTable();
    }

    public void CloseHelpMenu()
    {
        audioSource.PlayOneShot(SlideSFX);
        helpMenuAnimator.Play(closeAnimationName);
        GameManager.instance.ActivateCursor(false);
        storyManager.EnableUIElements();
        Invoke(nameof(CloseMenu), 1f);
    }

    public void ShowBinTable()
    {
        helpText.text = string.Empty;
        helpText.text = messages[0];
    }

    public void ShowBinAddMenu()
    {
        helpText.text = string.Empty;
        helpText.text = messages[1];
    }

    public void MovementControls()
    {
        helpText.text = string.Empty;
        helpText.text = messages[2];
    }

    public void UnwantedStiuations()
    {
        helpText.text = string.Empty;
        helpText.text = messages[3];
    }

    public void AboutInfo()
    {
        helpText.text = string.Empty;
        helpText.text = messages[4];
    }

    void CloseMenu()
    {
        helpMenu.SetActive(false);
    }
}

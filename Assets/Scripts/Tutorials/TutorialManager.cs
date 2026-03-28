using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private PlayerInputActions playerInputs;

    public GameObject tutorialUI;

    [Header("Image reference")]
    public Image image;
    public Sprite[] tutorialImages;

    [Header("Texts")]
    public TextMeshProUGUI tmpText;
    public string[] tutorialTexts;

    private int count = 0;

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
    }

    private void Start()
    {
        // Initialize the first page
        UpdateUI();
    }

    private void OnEnable()
    {
        playerInputs.Tutorials.Enable();
        playerInputs.Tutorials.Next.performed += NextPage;
        playerInputs.Tutorials.Previous.performed += PreviousPage;
        playerInputs.Tutorials.Open.performed += OpenTutorial;
    }

    private void OnDisable()
    {
        playerInputs.Tutorials.Open.performed -= OpenTutorial;
        playerInputs.Tutorials.Next.performed -= NextPage;
        playerInputs.Tutorials.Previous.performed -= PreviousPage;
        playerInputs.Tutorials.Disable();
    }

    // No need for ReadValueAsButton or booleans here, 
    // just calling the function is enough for a button tap.
    void NextPage(InputAction.CallbackContext context)
    {
        SwitchToNext();
    }

    void PreviousPage(InputAction.CallbackContext context)
    {
        SwitchToPrevious();
    }

    void OpenTutorial(InputAction.CallbackContext context)
    {
        GameManager.instance.ActivateCursor(true);
        GameManager.instance.DisableCameraAndMovementControls();
        tutorialUI.SetActive(true);
    }

    public void SwitchToNext()
    {
        // tutorialImages.Length - 1 is the index of the last item
        if (count < tutorialImages.Length - 1)
        {
            count++;
            UpdateUI();
        }
    }

    public void SwitchToPrevious()
    {
        if (count >= 0)
        {
            count--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // Safety check to ensure index is valid
        if (count >= 0 && count < tutorialImages.Length)
        {
            image.sprite = tutorialImages[count];
        }

        // Safety check for text array (in case it has a different size than images)
        if (count >= 0 && count < tutorialTexts.Length)
        {
            tmpText.text = tutorialTexts[count];
        }
    }
}
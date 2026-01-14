using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MessageSystem : MonoBehaviour
{
    private GameObject messagePanel;
    private Animator messageAnimator;
    private TextMeshProUGUI messageText;
    private PlayerInputActions messageInteraction;

    [Header("Animation Settings")]
    public string forwardAnimationName = "FrothAnimation";
    public string backwardAnimationName = "BackAnimation";

    [Header("Settings")]
    public string message;
    public float delay;

    private void Awake()
    {
        messageInteraction = new PlayerInputActions();

        messagePanel = GetComponent<GameObject>();
        messageText = GetComponentInChildren<TextMeshProUGUI>();
        messageAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        messageText.text = string.Empty;
        messageText.text = message;
    }

    private void OnEnable()
    {
        messageInteraction.Player.QuestBox.Enable();
        messageInteraction.Player.QuestBox.performed += OpenQuestMenu;
        StartCoroutine (HandleMessages());
    }

    IEnumerator HandleMessages()
    {
        messageAnimator.Play(forwardAnimationName);

        yield return new WaitForSeconds(delay);

        messageAnimator.Play(backwardAnimationName);
        yield return null;
    }

    void OpenQuestMenu(InputAction.CallbackContext ctx)
    {
        ctx.ReadValueAsButton();
        messageText.text = string.Empty;
        messageText.text = "Deactivate Core in 10 Minutes";
        StartCoroutine(HandleMessages());
    }
}

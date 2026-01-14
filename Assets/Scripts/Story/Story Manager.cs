using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public GameObject storyBoard;
    public GameObject info;

    public GameObject[] otherUIElements;

    private void Awake()
    {
        storyBoard.SetActive(false);
        info.SetActive(false);
    }

    private void Start()
    {
        GameManager.instance.DisableCameraAndMovementControls();
        DIsableUIElements();
        storyBoard.SetActive(true);
        Invoke(nameof(DisableStoryBoard), 4f);
    }

    void DisableStoryBoard()
    {
        storyBoard.SetActive(false);
        OpenInfo();
    }

    public void EnableUIElements()
    {
        for (int i = 0; i < otherUIElements.Length; i++)
        {
            otherUIElements[i].SetActive(true);
        }
    }

    public void DIsableUIElements()
    {
        for (int i = 0; i < otherUIElements.Length; i++)
        {
            otherUIElements[i].SetActive(false);
        }
    }
    public void OpenInfo()
    {
        GameManager.instance.ActivateCursor(true);
        info.SetActive(true);
    }

    public void CloseInfo()
    {
        GameManager.instance.EnableCameraAndMovementControls();
        GameManager.instance.ActivateCursor(false);
        info.SetActive(false);
        EnableUIElements();
    }
}
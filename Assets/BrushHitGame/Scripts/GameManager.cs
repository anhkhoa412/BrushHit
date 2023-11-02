using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isStart = false;
    public bool isWin = false;
    public bool isNormalMode;
    public int playerScore;
    public int playerColorCount = 0;
    public int enemyColorCount = 0;
    public AudioSource hitSound;
    public bool isMute = false;

    private int UILayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        LeanTween.init(800);
    }

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            isStart = true;
        }
    }

    public void GameOver()
    {
        AnchorManager[] anchorManagers = FindObjectsOfType<AnchorManager>();
        foreach (AnchorManager anchorManager in anchorManagers)
        {
            anchorManager.orbitSpeed = 0;
        }

        LeanTween.init(60);

        UIManager.Instance.gameOverPanel.SetActive(true);

        LeanTween.scale(UIManager.Instance.gameOverPanel, new Vector3(1, 1, 1), 1f)
            .setEase(LeanTweenType.easeInOutQuad)
            .setOnComplete(() =>
            {
                Time.timeScale = 0f;
            });
    }

    public void Win()
    {
        isWin = true;
    }

    // Returns 'true' if we touched or hovered over a Unity UI element.
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    // Returns 'true' if we touched or hovered over a Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
    {
        foreach (RaycastResult result in eventSystemRaycastResults)
        {
            if (result.gameObject.layer == UILayer)
            {
                return true;
            }
        }
        return false;
    }

    // Gets all event system raycast results at the current mouse or touch position.
    private static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}

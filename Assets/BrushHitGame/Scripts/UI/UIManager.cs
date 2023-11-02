using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public RectTransform transit;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }

      
    }
    void Start()
    {
        transit.gameObject.SetActive(true);
       
        LeanTween.scale(transit, new Vector3(1, 1, 1), 0);
        LeanTween.scale(transit, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            transit.gameObject.SetActive(false);
        });
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void onRestart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void onExitClick()
    {
        Application.Quit();
    }
}

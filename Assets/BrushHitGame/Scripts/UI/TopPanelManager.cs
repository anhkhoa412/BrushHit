using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TopPanelManager : MonoBehaviour
{
    private float startTime = 0f;      // The starting time for the count-up timer
    public float elapsedTime;
    [SerializeField] private Text time;
    [SerializeField] private Text score;

    [SerializeField] private Text playerScore;
    [SerializeField] private Text enemyScore;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button soundButton;
    public Sprite soundOnImage;
    public Sprite soundOffImage;

    [SerializeField] private ToggleGroup smallLevel;
    [SerializeField] private LevelSelector levelSelector;
    [SerializeField] private int currentLevelIndex = 1;
    [SerializeField] private Text currentLevel;
    [SerializeField] private Text nextLevel;

    void Start()
    {

        elapsedTime = startTime; // Initialize the elapsed time
        levelSelector = FindObjectOfType<LevelSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isStart){
           
            
            if (GameManager.Instance.isNormalMode)
            {
                score.text = GameManager.Instance.playerScore.ToString();
            } else
                elapsedTime += Time.deltaTime;
        }
        CheckToggleStates();

        if (!GameManager.Instance.isNormalMode)
        {
            UpdateTime();
        }

        if (!GameManager.Instance.isNormalMode)
        {
            playerScore.text = GameManager.Instance.playerColorCount.ToString();
            enemyScore.text = GameManager.Instance.enemyColorCount.ToString();
        }

        currentLevel.text = currentLevelIndex.ToString();
        nextLevel.text = (currentLevelIndex +1).ToString();
        UpdateButtonImage();
    }

    void UpdateTime()
    {

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void OnRestart()
    {
        levelSelector.RestartCurrentLevel();
        score.text = GameManager.Instance.playerScore.ToString();
    }

    public void CheckToggleStates()
    {
        Toggle[] toggles = smallLevel.GetComponentsInChildren<Toggle>();
        bool allTogglesChecked = true;

        for (int i = 0; i < toggles.Length; i++)
        {
            if (levelSelector.currentLevelIndex % 4 == i && !toggles[i].isOn)
            {
                if (GameManager.Instance.isWin)
                {
                    toggles[i].isOn = true;
                }
                toggles[i].GetComponent<Image>().enabled = true;

            }
           
            if (toggles[i].isOn)
            {
                toggles[i].GetComponent<Image>().enabled = false;
            } else
            {
                allTogglesChecked = false;
            }

            

        }
        if (allTogglesChecked)
        {
            

            for (int i = 0; i < toggles.Length; i++)
            {
                toggles[i].isOn = false;
                
            }
            currentLevelIndex++;

        }
    }

    public void OnSoundClick()
    {
        GameManager.Instance.isMute = !GameManager.Instance.isMute;
    }

    private void UpdateButtonImage()
    {
        if (!GameManager.Instance.isMute)
        {
            soundButton.GetComponent<Image>().sprite = soundOnImage;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOffImage;
        }
    }



}


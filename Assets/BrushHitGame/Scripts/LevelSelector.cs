using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject[] levels; // Array of level prefabs
    public int currentLevelIndex = 0; // Index of the current level
    private GameObject currentLevel; // Reference to the current level
    private int currentScore = 0; //Current score of the player.

    void Start()
    {
       
        // Instantiate the initial level (level 0)
        if (levels.Length > 0)
        {
            InstantiateLevel(currentLevelIndex);
        }
        else
        {
            
            Debug.LogError("No levels assigned in the LevelSelector script.");
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isWin) // Check the win condition from GameManager
        {
            // Check if there's another level to switch to
            if (currentLevelIndex < levels.Length - 1)
            {
                currentScore = GameManager.Instance.playerScore;
                currentLevelIndex++;
                InstantiateLevel(currentLevelIndex);
            }
            else
            {
                UIManager.Instance.winPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    void InstantiateLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
          
            Destroy(currentLevel);
        }

        currentLevel = Instantiate(levels[levelIndex]);
        GameManager.Instance.isStart = false;
        GameManager.Instance.isWin = false;
    }

    // Restart the current level to its initial state
    public void RestartCurrentLevel()
    {
        InstantiateLevel(currentLevelIndex);
        GameManager.Instance.isStart = false;
        GameManager.Instance.isWin = false;

        GameManager.Instance.playerScore = currentScore;
    }
}

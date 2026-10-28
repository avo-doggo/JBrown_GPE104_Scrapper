using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemies")]
    public List<PawnAI> Enemies = new List<PawnAI>();

    [Header("Scrap")]
    public List<CollectScrap> Scraps = new List<CollectScrap>();
    public List<CollectScrap> CollectedScrap = new List<CollectScrap>();

    [Header("Score")]
    public int score;
    public int requiredScore;

    public TMP_Text scoreTextUI;
    public TMP_Text scoreTextEndScreen;
    public TMP_Text winScoreText;

    [Header("Game State")]
    public bool gameOver;
    public bool levelWon;

    [Header("End Screens")]
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(
        Scene scene,
        LoadSceneMode mode)
    {
        Time.timeScale = 1f;

        score = 0;
        CollectedScrap.Clear();

        gameOver = false;
        levelWon = false;

        // Find the UI FIRST
        FindLevelUI();

        // Then find the level's objects
        FindEnemies();
        FindScrap();
        LoadLevelSettings();

        // Update UI after everything is found
        UpdateScoreUI();
    }

    /*public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("No more levels!");
        }
    }*/


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Start
    private void Start()
    {
        FindEnemies();
        FindScrap();
        LoadLevelSettings();

        UpdateScoreUI();
    }

    private void FindLevelUI()
    {
        LevelUI levelUI = FindFirstObjectByType<LevelUI>();

        if (levelUI != null)
        {
            gameOverPanel = levelUI.gameOverPanel;
            winPanel = levelUI.winPanel;

            scoreTextUI = levelUI.scoreTextUI;

            Debug.Log("Level UI found!");
        }
        else
        {
            Debug.LogWarning("No LevelUI found in this scene!");
        }

        if (gameOverPanel == null)
        {
            Debug.LogWarning("Game Over Panel is not assigned!");
        }

        if (winPanel == null)
        {
            Debug.LogWarning("Win Panel is not assigned!");
        }

        if (scoreTextEndScreen == null)
        {
            Debug.LogWarning("End Screen Score Text is not assigned!");
        }

        if (scoreTextUI == null)
        {
            Debug.LogWarning("Score Text UI is not assigned!");
        }
    }

    //FIND ENEMIES

    public void FindEnemies()
    {
        Enemies.Clear();

        PawnAI[] foundEnemies =
            FindObjectsByType<PawnAI>();

        foreach (PawnAI enemy in foundEnemies)
        {
            Enemies.Add(enemy);
        }

        Debug.Log("Enemies found: " + Enemies.Count);
    }


    //FIND SCRAP

    public void FindScrap()
    {
        Scraps.Clear();

        CollectScrap[] foundScrap =
            FindObjectsByType<CollectScrap>(FindObjectsSortMode.None);

        foreach (CollectScrap scrap in foundScrap)
        {
            Scraps.Add(scrap);
        }

        Debug.Log("Scrap in Level: " + Scraps.Count);
    }


    //ADD SCORE

    public void AddScore(int amount)
    {
        score += amount;

        UpdateScoreUI();

        Debug.Log("Score: " + score);
    }


    void UpdateScoreUI()
    {
        if (scoreTextUI != null)
        {
            scoreTextUI.text =
                score + " / " + requiredScore;
        }
    }


    //REMOVE SCRAP

    public void RemoveScrap(CollectScrap scrap)
    {
        if (scrap != null && Scraps.Contains(scrap))
        {
            Scraps.Remove(scrap);
        }
    }


    //COLLECT SCRAP

    public void AddToCollected(CollectScrap collectedScrap)
    {
        CollectedScrap.Add(collectedScrap);

        Debug.Log(
            "Collected Scrap: " +
            CollectedScrap.Count
        );
    }


    //PLAYER CAUGHT

    public void PlayerCaught()
    {
        if (gameOver || levelWon)
        {
            return;
        }

        gameOver = true;

        Debug.Log("PLAYER CAUGHT!");

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }


    //PLAYER WINS

    public void WinLevel()
    {
        if (gameOver || levelWon)
        {
            return;
        }

        levelWon = true;

        Debug.Log("LEVEL COMPLETE!");

        if (winScoreText != null)
        {
            winScoreText.text = "Score: " + score;
        }

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }


    //CHECK SCORE

    public bool HasEnoughPoints()
    {
        return score >= requiredScore;
    }

    public void LoadLevelSettings()
    {
        LevelSettings settings = FindFirstObjectByType<LevelSettings>();

        if (settings != null)
        {
            requiredScore = settings.requiredScore;

            Debug.Log("Required score for this level: " + requiredScore);
        }
        else
        {
            Debug.LogWarning("No LevelSettings found!");
        }

        UpdateScoreUI();
    }
}


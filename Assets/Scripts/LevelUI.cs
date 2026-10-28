using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;

    public TMP_Text winScoreText;

    public TMP_Text scoreTextUI;


    public void NextLevel()
    {
        Time.timeScale = 1f;

        int nextScene =
            SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("There are no more levels!");
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

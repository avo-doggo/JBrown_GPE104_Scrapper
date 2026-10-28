using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;

    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private bool isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        // Load saved master volume
        if (masterVolumeSlider != null)
        {
            float masterVolume =
                PlayerPrefs.GetFloat("MasterVolume", 0f);

            musicVolumeSlider.value = masterVolume;

            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // Load saved music volume
        if (musicVolumeSlider != null)
        {
            float musicVolume =
                PlayerPrefs.GetFloat("MusicVolume", 0f);

            musicVolumeSlider.value = musicVolume;

            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // Load saved SFX volume
        if (sfxVolumeSlider != null)
        {
            float sfxVolume =
                PlayerPrefs.GetFloat("SFXVolume", 0f);

            sfxVolumeSlider.value = sfxVolume;

            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        ResumeGame();
    }

    private void Update()
    {
        // Keyboard
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Controller
        if (Gamepad.current != null &&
            Gamepad.current.startButton.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;

        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void SetMasterVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMasterVolume(volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSFXVolume(volume);
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
}
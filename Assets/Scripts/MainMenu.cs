using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject pressAnyButtonPanel;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    [Header("Main Menu First Button")]
    public GameObject firstMainMenuButton;

    [Header("Settings First Button")]
    public GameObject firstSettingsButton;

    [Header("Credits First Button")]
    public GameObject firstCreditsButton;

    private bool pressAnyButtonScreen = true;

    private void Start()
    {
        // Start on the Press Any Button screen
        pressAnyButtonPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        pressAnyButtonScreen = true;
    }

    private void Update()
    {
        if (pressAnyButtonScreen)
        {
            CheckForAnyButton();
        }
    }

    private void CheckForAnyButton()
    {
        // Keyboard or mouse
        if (Keyboard.current != null &&
            Keyboard.current.anyKey.wasPressedThisFrame)
        {
            OpenMainMenu();
            return;
        }

        // Controller
        if (Gamepad.current != null &&
            Gamepad.current.wasUpdatedThisFrame)
        {
            // Ignore stick movement.
            // Require an actual button press.
            if (Gamepad.current.buttonSouth.wasPressedThisFrame ||
                Gamepad.current.buttonNorth.wasPressedThisFrame ||
                Gamepad.current.buttonEast.wasPressedThisFrame ||
                Gamepad.current.buttonWest.wasPressedThisFrame ||
                Gamepad.current.startButton.wasPressedThisFrame ||
                Gamepad.current.selectButton.wasPressedThisFrame)
            {
                OpenMainMenu();
            }
        }
    }

    private void OpenMainMenu()
    {
        pressAnyButtonScreen = false;

        pressAnyButtonPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(
            firstMainMenuButton
        );
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_1");
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(
            firstSettingsButton
        );
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(
            firstMainMenuButton
        );
    }

    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(
            firstCreditsButton
        );
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(
            firstMainMenuButton
        );
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
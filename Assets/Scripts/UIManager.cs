using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private GameObject m_SettingsMenu;
    [SerializeField] private GameObject m_ControlsMenu;
    [SerializeField] private CharacterController m_CharacterController;

    
    public bool IsPaused;

    private void Start() {
        IsPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame(){
        m_PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame(){
        m_PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Respawn(){
        m_CharacterController.RestartCharacters();
        m_PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void SettingsMenu()
    {
        m_SettingsMenu.SetActive(true);
        m_ControlsMenu.SetActive(false);
    }

    public void ControlsMenu()
    {
        m_ControlsMenu.SetActive(true);
    }

    public void StartGame()
    {
        //change scenename later
        SceneManager.LoadScene("ShawnTesting");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private GameObject m_SettingsMenu;
    [SerializeField] private GameObject m_ControlsMenu;
    [SerializeField] private CharacterController m_CharacterController;

    public GameObject InfoText;
    public bool m_ThrowIntroText;
    public bool SwapIntroText;
    public bool GrabIntroText;
    public bool IsPaused;

    private void Start() {
        IsPaused = false;
        Time.timeScale = 1f;
        m_ThrowIntroText = true;
        SwapIntroText = true;
        GrabIntroText = false;
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
        //Loads Cinematic Scene at position 1
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        //Load Level Scene
        SceneManager.LoadScene(2);
    }

    public void WinGame()
    {
        //Load Win Scene at position 2
        SceneManager.LoadScene(3);
    }

    public void ChangeText(string newText)
    {
        if(!InfoText.activeSelf)
        {
            InfoText.SetActive(true);
        }
        InfoText.GetComponent<Text>().text = newText;

    }

    public void ToggleText()
    {
        if(InfoText.activeSelf){
            InfoText.SetActive(false);
        }
        else
        {
            InfoText.SetActive(true);
        }
    }
}

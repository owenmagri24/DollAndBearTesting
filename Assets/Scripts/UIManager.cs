using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PauseMenu;

    [SerializeField]
    private CharacterController m_CharacterController;

    public bool IsPaused;

    private void Start() {
        IsPaused = false;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Characters = {};

    [SerializeField]
    private CharacterController m_CharacterController;

    private void Update() {
        for (int i = 0; i < m_Characters.Length; i++)
        {
            if(m_Characters[i].transform.position.y < -20){
                //Player falls off the map and loses
                m_CharacterController.RestartCharacters();
            }
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public CharacterBase m_ControlledCharacter = null;

    public CharacterBase[] m_Characters = {};

    [SerializeField]
    private UIManager m_UIManager;

    [SerializeField]
    private GameObject m_Ball;

    [SerializeField]
    private CinemachineVirtualCamera m_Camera;

    private BallControl m_BallControl;

    void OnMovement(InputValue value)
        => m_ControlledCharacter.OnMovement(value);

    void OnJump()
        => m_ControlledCharacter.OnJump();

    void OnInteract()
        => m_ControlledCharacter.OnInteract();

    private void Awake() {
        m_BallControl = m_Ball.GetComponent<BallControl>();
    }
    private void Start() {
        RestartCharacters();
    }

    private void Update() {
        //if bear is active, deactivate ball
        if (m_Characters[1].gameObject.activeSelf || m_UIManager.IsPaused == true)
        {
            m_Ball.SetActive(false);
        }
        else{
            m_Ball.SetActive(true);
        }
    }

    void OnSwitchPlayer(){
        if (!m_Characters[1].gameObject.activeSelf)
            return;//exit function if bear is not active

        m_ControlledCharacter.Stop();

        if(m_ControlledCharacter == m_Characters[0]){
            ControlBear();
        }else 
        {
            ControlGirl();
        }
    }

    void ControlGirl(){
        m_ControlledCharacter = m_Characters[0];
        m_Camera.Follow = m_Characters[0].transform;
    }

    void ControlBear(){
        m_ControlledCharacter = m_Characters[1];
        m_Camera.Follow = m_Characters[1].transform;
    }

    public void RestartCharacters(){
        //activate girl
        ControlGirl();
        m_Characters[1].gameObject.SetActive(false); //deactivate bear
        m_ControlledCharacter.gameObject.transform.position = m_ControlledCharacter.m_RespawnPoint; //teleport to latest respawn point

        if(m_Ball.transform.parent == null) //if ball is in the air
        {
            m_BallControl.ResetBall(); //reset ball
            m_Camera.m_Lens.OrthographicSize = m_BallControl.m_ZoomNormal; //normal zoom
        }
    }

    void OnPause()
    {
        if(m_UIManager.IsPaused == false){
            m_UIManager.PauseGame();
        }
        else{
            m_UIManager.ResumeGame();
        }
    }
}

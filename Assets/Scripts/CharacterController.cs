using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterBase m_ControlledCharacter = null;

    public CharacterBase[] m_Characters = {};

    [SerializeField]
    private GameObject m_Ball;

    [SerializeField]
    private CinemachineVirtualCamera m_Camera;

    void OnMovement(InputValue value)
        => m_ControlledCharacter.OnMovement(value);

    void OnJump()
        => m_ControlledCharacter.OnJump();

    void OnInteract()
        => m_ControlledCharacter.OnInteract();

    void Awake()
    {
        m_ControlledCharacter = m_Characters[0];
        m_Characters[1].gameObject.SetActive(false);
    }

    private void Update() {
        //if bear is active, deactivate ball
        if (m_Characters[1].gameObject.activeSelf)
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
        if(m_ControlledCharacter == m_Characters[0]){
            m_ControlledCharacter = m_Characters[1];
            m_Camera.Follow = m_Characters[1].transform;
        }else 
        {
            m_ControlledCharacter = m_Characters[0];
            m_Camera.Follow = m_Characters[0].transform;
        }
    }
}

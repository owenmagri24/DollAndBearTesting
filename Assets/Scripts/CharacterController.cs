using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterBase m_ControlledCharacter = null;

    [SerializeField]
    private CharacterBase[] m_Characters = {};

    [SerializeField]
    private GameObject m_Camera;
    CameraScript cameraScript;

    void OnMovement(InputValue value)
        => m_ControlledCharacter.OnMovement(value);

    void OnJump()
        => m_ControlledCharacter.OnJump();

    void Awake()
    {
        cameraScript = m_Camera.GetComponent<CameraScript>();
        m_ControlledCharacter = m_Characters[0];
    }
    void OnSwitchPlayer(){
        if(m_ControlledCharacter == m_Characters[0]){
            m_ControlledCharacter = m_Characters[1];
            cameraScript.m_Target = m_Characters[1].transform;
        }else 
        {
            m_ControlledCharacter = m_Characters[0];
            cameraScript.m_Target = m_Characters[0].transform;
        }
    }
}

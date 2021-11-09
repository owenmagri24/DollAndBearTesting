using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Girl;

    [SerializeField]
    private GameObject m_Doll;

    [SerializeField]
    private GameObject m_Camera;

    CameraScript cameraScript;

    void Start() {
        cameraScript = m_Camera.GetComponent<CameraScript>();
        m_Doll.GetComponent<DollScript>().enabled = false;
    }
    void OnSwitchPlayer(){
        if(m_Girl.GetComponent<GirlScript>().enabled == true){
            //Switch to Doll
            m_Girl.GetComponent<GirlScript>().enabled = false;
            m_Doll.GetComponent<DollScript>().enabled = true;
            cameraScript.m_Target = m_Doll.transform;
        }else{
            //Switch to Girl
            m_Doll.GetComponent<DollScript>().enabled = false;
            m_Girl.GetComponent<GirlScript>().enabled = true;
            cameraScript.m_Target = m_Girl.transform;
        }
    }
}

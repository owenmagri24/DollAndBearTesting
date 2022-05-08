using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateGlobe : MonoBehaviour
{
    [SerializeField] private Transform m_Globe;
    [SerializeField] private GameObject m_GlobeDeathArea;
    [SerializeField] private float m_MaxRotationSpeed;
    [SerializeField] private float m_RotationSpeedDecrease;
    private float m_RotationSpeed;
    private bool m_CanRotate;

    private void Start() {
        m_RotationSpeed = m_MaxRotationSpeed;
        m_CanRotate = true;
    }

    private void Update() {
        if(m_CanRotate)
        {
            m_Globe.Rotate(0,0,m_RotationSpeed);
        }
        else
        {
            if(m_RotationSpeed >= 0)
            {
                m_RotationSpeed -= m_RotationSpeedDecrease * Time.deltaTime;
                m_Globe.Rotate(0,0,m_RotationSpeed);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            m_CanRotate = false;
            m_GlobeDeathArea.SetActive(false);
        }
    }
}

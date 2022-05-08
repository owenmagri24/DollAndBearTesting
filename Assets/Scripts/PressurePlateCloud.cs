using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateCloud : MonoBehaviour
{
    [SerializeField]
    private CloudLift m_CloudLift;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            m_CloudLift.m_CanMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            m_CloudLift.m_CanMove = false;
        }
    }
}

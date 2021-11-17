using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private Rigidbody2D m_Rb;

    private void Start() {
        m_Rb = this.GetComponent<Rigidbody2D>();
        m_Rb.freezeRotation = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Water")
        {
            m_Rb.freezeRotation = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Water")
        {
            m_Rb.freezeRotation = false;
        }
    }
}

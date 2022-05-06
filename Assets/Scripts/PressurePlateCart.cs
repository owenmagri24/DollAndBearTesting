using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateCart : MonoBehaviour
{
    [SerializeField]
    private MovingCartScript m_MovingCartScript;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            m_MovingCartScript.m_CanMove = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            m_MovingCartScript.m_CanMove = false;
        }
    }
}

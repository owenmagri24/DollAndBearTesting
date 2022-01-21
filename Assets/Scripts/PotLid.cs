using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotLid : MonoBehaviour
{
    [SerializeField]
    private Transform m_LidHolder;

    [SerializeField]
    private Animator m_SteamAnimator;
    
    [SerializeField]
    private GameObject m_SteamDeathBoundary;

    private Rigidbody2D m_Rb;

    private void Awake() {
        m_Rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Pot")
        {
            gameObject.transform.position = m_LidHolder.transform.position; //set lid position to lidholder position
            gameObject.transform.rotation = m_LidHolder.rotation; //set lid rotation to lidholder rotation
            m_Rb.constraints = RigidbodyConstraints2D.FreezeAll; //freeze constraints
            m_SteamAnimator.SetBool("SteamOff", true); //turn steam off
            m_SteamDeathBoundary.SetActive(false); //turn off steam death boundary

        }
    }

}

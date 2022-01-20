using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlScript : CharacterBase
{
    [SerializeField]
    private BallControl m_BallControl;

    private SleepingBearScript m_SleepingBearScript;

    [SerializeField]
    private GameObject m_ThrowText;


    public override void Update()
    {
        base.Update();
        ThrowText();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        
        if(m_ObjectHit != null &&  m_ObjectHit.tag == "Bear"){
            m_ObjectHit.SetActive(false);
        }
        else if(m_ObjectHit != null &&  m_ObjectHit.name == "SleepingBear")
        {
            m_BallControl.m_canThrow = true;
            Destroy(m_ObjectHit);
        }
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if(other.gameObject.GetComponent<SleepingBearScript>())
        {
            m_SleepingBearScript = other.gameObject.GetComponent<SleepingBearScript>();
            m_SleepingBearScript.ToggleText();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetComponent<SleepingBearScript>())
        {
            m_SleepingBearScript.ToggleText();
        }
    }

    public void StopMovement()
    {
        //freezes Horizontal movement and Z Rotation
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void EnableMovement()
    {
        //removes constrains except Z rotation
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void ThrowText()
    {
        if(m_ThrowText != null && m_BallControl.m_canThrow)
        {
            if(m_BallControl.m_isThrowing)
            {
                m_ThrowText.SetActive(false);
                Destroy(m_ThrowText);
            }
            else
            {
                m_ThrowText.SetActive(true);
            }
        }
    }
}

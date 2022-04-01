using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlScript : CharacterBase
{

    private SleepingBearScript m_SleepingBearScript;


    public override void Update()
    {
        base.Update();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        
        if (m_ObjectHit != null &&  m_ObjectHit.tag == "Bear"){
            m_ObjectHit.transform.parent = null;
            m_ObjectHit.SetActive(false);

            if(m_UIManager.SwapIntroText == true)
            {
                m_UIManager.ToggleText();
                m_UIManager.SwapIntroText = false;
            }
        }

        //Intro Segment
        else if(m_ObjectHit != null &&  m_ObjectHit.name == "SleepingBear")
        {
            m_BallControl.m_canThrow = true;
            Destroy(m_ObjectHit);
            m_UIManager.ChangeText("Left click to throw the bear");
        }
        m_ObjectHit = null;
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.gameObject.GetComponent<SleepingBearScript>())
        {
            m_SleepingBearScript = other.gameObject.GetComponent<SleepingBearScript>();
            m_UIManager.ToggleText();
        }
        else if(other.gameObject.tag == "Level1End")
        {
            //win game
            m_UIManager.WinGame();
        }
        else if(other.gameObject.tag == "PowerUP_Throw")
        {
            m_BallControl.powerUpThrow = true;
            Destroy(other.gameObject);
        }
    }
    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if(other.gameObject.GetComponent<SleepingBearScript>())
        {
            m_UIManager.ToggleText();
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
}

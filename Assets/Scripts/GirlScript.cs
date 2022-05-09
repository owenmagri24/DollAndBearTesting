using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlScript : CharacterBase
{
    
    private GameObject m_SleepingBear;

    public override void Start() {
        base.Start();
        

        m_SleepingBear = GameObject.FindGameObjectWithTag("SleepingBear");

        if(m_SleepingBear == null) 
            m_BallControl.m_canThrow = true;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        
        if (m_ObjectHit != null &&  m_ObjectHit.tag == "Bear"){
            m_ObjectHit.transform.parent = null;
            m_ObjectHit.SetActive(false);

            if(m_UIManager.m_IsIntro == true && m_UIManager.SwapIntroText == true)
            {
                m_UIManager.ToggleText();
                m_UIManager.SwapIntroText = false;
            }
        }

        //Intro Segment
        else if(m_ObjectHit != null &&  m_ObjectHit == m_SleepingBear)
        {
            m_BallControl.m_canThrow = true;
            Destroy(m_ObjectHit);
            m_UIManager.ChangeText("Left click to throw the bear");
        }
        m_ObjectHit = null;
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.gameObject == m_SleepingBear)
        {
            m_UIManager.ToggleText();
        }
        else if(other.gameObject.tag == "Level1End")
        {
            //load next level
            m_UIManager.LoadLevel2();
        }
        else if(other.gameObject.tag == "Level2End")
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

        if(other.gameObject == m_SleepingBear)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollScript : CharacterBase
{
    
    protected override void Update()
    {
        base.Update();
        
    }

    public override void OnInteract(){
        base.OnInteract();

        if(m_hit.collider != null){
            m_ObjectHit = m_hit.collider.gameObject;

            if(m_ObjectHit.tag == "PushableObject" && m_HoldingObject == false)
            {
                //Picking up Pushable Objects

                Physics2D.IgnoreCollision(m_hit.collider, m_BoxCollider2D); //ignores picked up object collider
                m_HoldingObject = true;
                m_ObjectHit.transform.parent = m_BoxHolder;
                m_ObjectHit.transform.position = m_BoxHolder.position;
                m_ObjectHit.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else if(m_HoldingObject == true)
            {
                //Releasing Pushable Objects

                Physics2D.IgnoreCollision(m_hit.collider, m_BoxCollider2D, false);//enables collision on release
                m_HoldingObject = false;
                m_ObjectHit.transform.parent = null;
                m_ObjectHit.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}

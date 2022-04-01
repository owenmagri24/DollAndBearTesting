using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollScript : CharacterBase
{
    [SerializeField]
    private BoxCollider2D m_BoxHolderCollider;
    private BoxCollider2D objectHitCollider;

    [SerializeField]
    private PhysicsMaterial2D m_NoFrictionMaterial;
    private float m_ObjectHeight;

    public override void Update()
    {
        base.Update();
        animator.SetBool("Holding", m_HoldingObject != null);
    }

    public override void OnInteract(){
        base.OnInteract();

        
        
        if(m_ObjectHit != null && m_ObjectHit.tag == "PushableObject" && m_HoldingObject == null)
        { 
            

            // Physics2D.IgnoreCollision(m_hit.collider, m_BoxCollider2D); //ignores picked up object collider
            m_HoldingObject = m_ObjectHit.transform;
            m_ObjectHit.transform.parent = m_BoxHolder;
            m_ObjectHit.transform.position = m_BoxHolder.position;
            
            m_ObjectHit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            m_ObjectHit.GetComponent<Rigidbody2D>().isKinematic = true;
            objectHitCollider = m_ObjectHit.GetComponent<BoxCollider2D>();

            m_ObjectHeight = objectHitCollider.size.y;//get height of object

            objectHitCollider.enabled = false; //disable object collider
            m_BoxHolderCollider.enabled = true; //enable boxholder collider

            //add height/2 to boxholder collider y position
            m_BoxHolderCollider.transform.position = new Vector3(m_BoxHolder.transform.position.x , m_BoxHolder.transform.position.y + (m_ObjectHeight/2), m_BoxHolder.transform.position.z);
            
            m_BoxHolderCollider.sharedMaterial = m_NoFrictionMaterial;
            m_BoxHolderCollider.size = objectHitCollider.size; //set boxholder collider size to object collider size

            //Intro Segment
            if(m_UIManager.GrabIntroText == true)
            {
                m_UIManager.GrabIntroText = false;
                m_UIManager.ToggleText();
            }
        }
        else if(m_HoldingObject != null)
        {
            m_BoxHolderCollider.transform.position = new Vector3(m_BoxHolder.transform.position.x , m_BoxHolder.transform.position.y - (m_ObjectHeight/2), m_BoxHolder.transform.position.z);
            //Releasing Pushable Objects
            m_BoxHolderCollider.sharedMaterial = null;
            m_BoxHolderCollider.enabled = false; //disable boxholder collider
            objectHitCollider.enabled = true; //enable object collider
            // Physics2D.IgnoreCollision(m_hit.collider, m_BoxCollider2D, false);//enables collision on release
            m_ObjectHit.transform.parent = null;
            m_ObjectHit.GetComponent<Rigidbody2D>().isKinematic = false;
            m_ObjectHit = null;
            m_HoldingObject = null;
        }
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.gameObject.tag == "Level1End")
        {
            m_UIManager.ChangeText("Where's the girl?");
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if(other.gameObject.tag == "Level1End")
        {
            m_UIManager.ToggleText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollScript : CharacterBase
{
    [Header("Doll Variables")]
    [SerializeField]
    private BoxCollider2D m_BoxHolderCollider;
    private BoxCollider2D objectHitCollider;

    [SerializeField]
    private PhysicsMaterial2D m_NoFrictionMaterial;
    private float m_ObjectHeight;

    public PressurePlate.PlateColor m_BearColor; //used in pressureplate script

    [Header("WallJumping")]
    [SerializeField] private Transform m_FrontCheck;
    [SerializeField] private LayerMask m_WallLayer;
    [SerializeField] private float m_WallSlidingSpeed;
    [SerializeField] private float m_WallCheckRadius;
    [SerializeField] private float m_xWallForce;
    [SerializeField] private float m_yWallForce;
    [SerializeField] private float m_WallJumpTime;
    private bool m_IsFrontTouchingWall;
    private bool m_IsWallSliding;
    private bool m_IsWallJumping;
    public bool m_CanWallJump = false;


    public override void Update()
    {
        base.Update();
        animator.SetBool("Holding", m_HoldingObject != null);
        if (m_CanWallJump == true)
        {
            WallJumping();
        }
    }

    public override void OnInteract(){
        base.OnInteract();
        
        if(m_ObjectHit != null && m_ObjectHit.tag == "PushableObject" && m_HoldingObject == null)
        {

            // Physics2D.IgnoreCollision(m_hit.collider, m_BoxCollider2D); //ignores picked up object collider
            m_HoldingObject = m_ObjectHit.transform;
            m_ObjectHit.transform.rotation = Quaternion.Euler(0,0,0);
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
            if(m_UIManager.GrabIntroText == true && m_UIManager.m_IsIntro == true)
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
            m_ObjectHit.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 5f, 0f);
            
            m_ObjectHit = null;
            m_HoldingObject = null;
        }
    }

    public override void OnJump()
    {
        base.OnJump();

        if(m_IsWallSliding)
        {
            m_IsWallJumping = true;
            Invoke("SetWallJumpingToFalse", m_WallJumpTime);
        }
    }


    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if(other.gameObject.tag == "Level1End")
        {
            m_UIManager.ChangeText("Where's the girl?");
        }

        if(other.gameObject.name == "Lock")
        {
            if(m_HoldingObject != null && m_HoldingObject.name == "Key")
            {
                other.GetComponent<Lock>().OpenChestTop();
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if(other.gameObject.tag == "Level1End")
        {
            m_UIManager.ToggleText();
        }

    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);

        if (other.gameObject.tag == "PowerUP_WallJump")
        {
            m_UIManager.ChangeLevel2Text("The bear can now Wall Jump on certain walls!");
            m_CanWallJump = true;
            Destroy(other.gameObject);
        }
    }

    private void WallJumping()
    {
        m_IsFrontTouchingWall = Physics2D.OverlapCircle(m_FrontCheck.position, m_WallCheckRadius, m_WallLayer);

        if(m_IsFrontTouchingWall && !IsGrounded() && direction.x != 0)
        {
            m_IsWallSliding = true;
        }
        else
        {
            m_IsWallSliding = false;
        }

        if(m_IsWallSliding)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, Mathf.Clamp(m_Rigidbody2D.velocity.y, -m_WallSlidingSpeed, float.MaxValue));
        }

        if(m_IsWallJumping)
        {
            m_Rigidbody2D.velocity = new Vector2(m_xWallForce * direction.x, m_yWallForce);
        }
    }

    private void SetWallJumpingToFalse()
    {
        m_IsWallJumping = false;
    }
}

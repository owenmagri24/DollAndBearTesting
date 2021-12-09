using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour
{
    protected Rigidbody2D m_Rigidbody2D;

    protected Collider2D m_BoxCollider2D;

    [SerializeField]
    protected float m_JumpForce = 500f;
    
    [SerializeField]
    protected float m_Speed = 5f;

    protected Vector2 direction = Vector2.zero;

    [SerializeField]
    protected float m_InteractDistance = 1f;

    protected RaycastHit2D m_hit;

    protected GameObject m_ObjectHit;

    [SerializeField]
    protected Transform m_BoxHolder;
    protected bool m_HoldingObject = false;

    protected virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<Collider2D>();
    }

    protected virtual void FixedUpdate(){
        m_Rigidbody2D.velocity = new Vector2(direction.x * m_Speed, m_Rigidbody2D.velocity.y);
        //transform.Translate(direction * m_Speed * Time.deltaTime);
        Physics2D.queriesStartInColliders = false; //Avoids ray collisions from hitting own object
        m_hit = Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, m_InteractDistance);
    }

    public void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
        //character flipping
        Vector3 characterScale = transform.localScale;
        if(direction.x < 0){
            characterScale.x = -1;
        }
        if(direction.x > 0){
            characterScale.x = 1;
        }
        transform.localScale = characterScale;
    }

    public void OnJump(){
        if(IsGrounded()){
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    protected bool IsGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 2.5f, 1 << LayerMask.NameToLayer("Platforms"));
        Debug.Log(LayerMask.NameToLayer("Platforms"));
        return raycastHit2D.collider != null;
    }

    protected void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * m_InteractDistance);
    }

    public virtual void OnInteract(){
        if(m_hit.collider != null){
            m_ObjectHit = m_hit.collider.gameObject;

            
            /* -- to be used when implementing interactable objects
            if(m_ObjectHit.tag == "InteractableObject"){

            }
            */
        }
    }
}

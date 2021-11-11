using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected LayerMask m_PlatformsLayerMask;
    protected Rigidbody2D m_Rigidbody2D;

    protected BoxCollider2D m_BoxCollider2D;

    [SerializeField]
    protected float m_JumpForce = 500f;
    
    [SerializeField]
    protected float m_Speed = 5f;

    protected Vector2 direction = Vector2.zero;

    protected virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update(){
        transform.Translate(direction * m_Speed * Time.deltaTime);
    }

    public void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnJump(){
        if(IsGrounded()){
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    protected bool IsGrounded(){
        //BoxCast will only check for objects with Platforms Layer
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(m_BoxCollider2D.bounds.center, m_BoxCollider2D.bounds.size, 0f, Vector2.down, .1f, m_PlatformsLayerMask);
        return raycastHit2D.collider != null;
    }
}

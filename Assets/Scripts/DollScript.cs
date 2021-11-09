using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DollScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_PlatformsLayerMask;
    private Rigidbody2D m_Rigidbody2D;

    private BoxCollider2D m_BoxCollider2D;

    [SerializeField]
    private float m_DollJumpForce = 500f;
    
    [SerializeField]
    private float m_DollSpeed = 5f;
    Vector2 direction = Vector2.zero;

    void Start(){
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        transform.Translate(direction * m_DollSpeed * Time.deltaTime);
    }

    void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    private void OnJump(){
        if(IsGrounded()){
            m_Rigidbody2D.AddForce(new Vector2(0f, m_DollJumpForce));
        }
        
    }

    private bool IsGrounded(){
        //BoxCast will only check for objects with Platforms Layer
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(m_BoxCollider2D.bounds.center, m_BoxCollider2D.bounds.size, 0f, Vector2.down, .1f, m_PlatformsLayerMask);
        return raycastHit2D.collider != null;
    }
}

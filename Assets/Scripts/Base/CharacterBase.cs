using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;

    protected Rigidbody2D m_Rigidbody2D;

    protected Collider2D m_BoxCollider2D;

    [SerializeField]
    protected float m_JumpForce = 500f;

    [SerializeField]
    protected float m_JumpCheckToFloor = 1.01f;
    
    [SerializeField]
    protected float m_Speed = 5f;

    protected Vector2 direction = Vector2.zero;

    [SerializeField]
    protected float m_InteractDistance = 1f;

    protected RaycastHit2D m_hit;

    protected GameObject m_ObjectHit;

    [SerializeField]
    protected Transform m_BoxHolder;
    protected Transform m_HoldingObject = null;

    protected CharacterController m_CharacterController;
    public Vector2 m_RespawnPoint;
    protected GameObject m_RespawnPointsList;
    protected Transform m_StartingPoint;

    protected virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<Collider2D>();
        m_CharacterController = GameObject.Find("PlayerControls").GetComponent<CharacterController>();
        m_StartingPoint = GameObject.Find("RespawnPoint1").GetComponent<Transform>();
        m_RespawnPointsList = GameObject.Find("RespawnPoints");
    }

    protected void Start() {
        m_RespawnPoint = new Vector2(m_StartingPoint.position.x, m_StartingPoint.position.y);
    }

    public virtual void Update()
    {
        animator.SetBool("Running", direction.x != 0);
        animator.SetBool("Grounded", IsGrounded());
    }

    protected virtual void FixedUpdate(){
        m_Rigidbody2D.velocity = new Vector2(direction.x * m_Speed, m_Rigidbody2D.velocity.y);
        //transform.Translate(direction * m_Speed * Time.deltaTime);
        Physics2D.queriesStartInColliders = false; //Avoids ray collisions from hitting own object

        animator.SetFloat("Vertical Velocity", m_Rigidbody2D.velocity.y);
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
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
        }
    }

    protected bool IsGrounded(){
        int layers = 1 << LayerMask.NameToLayer("Platforms") | 1 << LayerMask.NameToLayer("InteractableObject");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, m_JumpCheckToFloor, layers);
        return raycastHit2D.collider != null;
    }

    protected void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * m_InteractDistance);
    }

    public virtual void OnInteract()
    {
        if (m_HoldingObject == null)
        {
            int layers = 1 << LayerMask.NameToLayer("InteractableObject") | 1 << LayerMask.NameToLayer("Player");
            m_hit = Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, m_InteractDistance, layers);

            if(m_hit.collider != null){
                m_ObjectHit = m_hit.collider.gameObject;
            }
        }
    }
    public void Stop()
    {
        direction = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Cushion")
        {
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce * 1.5f, ForceMode2D.Impulse);
        }
        else if(other.gameObject.name == "MovingCart")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.name == "MovingCart")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.IsChildOf(m_RespawnPointsList.transform))
        {
            for(int i = 0; i < m_CharacterController.m_Characters.Length; i++) 
            {
                //change respawn points for both characters
                m_CharacterController.m_Characters[i].m_RespawnPoint = new Vector2(other.transform.position.x, other.transform.position.y);
            }
        }
    }
}

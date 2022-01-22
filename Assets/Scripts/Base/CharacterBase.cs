using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected Animator animator;

    protected AudioManager m_AudioManager;

    protected RespawnPointManager m_RespawnPointManager;

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

    protected GameObject m_CushionsList;

    protected UIManager m_UIManager;

    protected BallControl m_BallControl;

    protected virtual void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<Collider2D>();
        m_RespawnPointManager = FindObjectOfType<RespawnPointManager>();
        m_BallControl = FindObjectOfType<BallControl>();
        m_UIManager = FindObjectOfType<UIManager>();
        m_CharacterController = GameObject.Find("PlayerControls").GetComponent<CharacterController>();
        m_CushionsList = GameObject.Find("CushionsParent");
        m_AudioManager = AudioManager.instance;
    }

    protected void Start() {
        if(m_RespawnPointManager.RespawnPointsLists != null)
        {
            m_RespawnPoint = m_RespawnPointManager.RespawnPointsLists[0].location; //get location of respawn point in position 0
        }
        else
        {
            Debug.LogWarning("RespawnPointsList is empty");
            return;
        }
        
    }

    public virtual void Update()
    {
        animator.SetBool("Running", direction.x != 0);
        animator.SetBool("Grounded", IsGrounded());
    }

    protected virtual void FixedUpdate() {
        m_Rigidbody2D.velocity = new Vector2(direction.x * m_Speed, m_Rigidbody2D.velocity.y);
        Physics2D.queriesStartInColliders = false; //Avoids ray collisions from hitting own object

        animator.SetFloat("Vertical Velocity", m_Rigidbody2D.velocity.y);
    }

    public void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
        //character flipping
        Vector3 characterScale = transform.localScale;
        if (direction.x < 0) {
            characterScale.x = -1;
        }
        if (direction.x > 0) {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;
    }

    public void OnJump() {
        if (IsGrounded()) {
            m_AudioManager.Play("PlayerJump");
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse);
            
        }
    }

    protected bool IsGrounded() {
        int layers = 1 << LayerMask.NameToLayer("Platforms") | 1 << LayerMask.NameToLayer("InteractableObject");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, m_JumpCheckToFloor, layers);
        return raycastHit2D.collider != null;
    }

    protected void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(transform.position.x, transform.position.y - 0.5f) + Vector2.right * transform.localScale.x * m_InteractDistance);
    }

    public virtual void OnInteract()
    {      
        if (m_HoldingObject == null)
        {
            int layers = 1 << LayerMask.NameToLayer("InteractableObject") | 1 << LayerMask.NameToLayer("Player");
            m_hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y -0.5f), Vector2.right * transform.localScale.x, m_InteractDistance, layers);
            
            if (m_hit.collider != null) {
                m_ObjectHit = m_hit.collider.gameObject;
            }
        }
    }
    public void Stop()
    {
        direction = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.transform.IsChildOf(m_CushionsList.transform) && (other.gameObject.name == ("CushionBigBounce")))
        {
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce * 2.5f, ForceMode2D.Impulse);
            m_AudioManager.Play("PillowJump");
        }
        else if(other.gameObject.transform.IsChildOf(m_CushionsList.transform))
        {
            m_Rigidbody2D.AddForce(Vector2.up * m_JumpForce * 1.5f, ForceMode2D.Impulse);
            m_AudioManager.Play("PillowJump");
        }
        
    }


    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.TryGetComponent<RespawnPoint>(out RespawnPoint rp)) //if other has respawnpoint script: set it as rp
        {
            if(!m_RespawnPointManager.RespawnPointsLists.Contains(rp)) // if rp is not in RespawnPointsList
            {
                m_RespawnPointManager.RespawnPointsLists.Add(rp); //add this respawnpoint to the list
                m_AudioManager.Play("Checkpoint"); 

                foreach(CharacterBase character in m_CharacterController.m_Characters)
                {
                    character.m_RespawnPoint = rp.location;//set position of both characters to rp
                }
            }
        }
        else if(other.gameObject.tag == "DeathArea")
        {
            //Debug.Log("death: " + other.gameObject.name);
            m_CharacterController.RestartCharacters();
        }
        else if(other.gameObject.tag == "MovingCart")
        {
            transform.parent = other.transform;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "MovingCart")
        {
            Debug.Log("exited");
            transform.parent = null;
        }
    }
}

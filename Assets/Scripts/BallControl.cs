using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{

    [SerializeField]
    private CharacterBase m_CharacterBase;
    public GameObject DollBear;
    public GameObject Girl;
    public SpriteRenderer BearBallVisual;

    [SerializeField]
    private float m_Power = 1f;
    
    private Rigidbody2D m_Rb;

    private LineRenderer m_Lr;

    private CircleCollider2D m_Cl;

    private Vector2 m_DragStartPos;

    private Vector2 m_mousePosition;

    public bool m_isThrowing = false;

    public bool m_canThrow = false;

    [SerializeField]
    private CinemachineVirtualCamera m_Camera;

    [SerializeField]
    private CinemachineTargetGroup m_TargetGroupCamera;

    [SerializeField]
    private CharacterController m_CharacterController;

    [SerializeField]
    public float m_ZoomOut = 15f;

    [SerializeField]
    public float m_ZoomNormal = 10f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    protected Animator animator;

    private AudioManager m_AudioManager;


    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        m_Lr = GetComponent<LineRenderer>();
        m_Cl = GetComponent<CircleCollider2D>();
        m_AudioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_Rb.isKinematic = true;
        m_Cl.enabled = false;
        BearBallVisual.enabled = false;
    }

    private void Update() {
        OnMouseMove();
        this.gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        DollBear.transform.position = transform.position;

        ResetBall();
        m_AudioManager.StopPlay("BearThrow");//stop bear throw sound if its still playing

        DollBear.SetActive(true);
        m_Camera.m_Follow = DollBear.transform;
        m_Camera.m_Lens.OrthographicSize = m_ZoomNormal; //Changes zoom back to normal
        BearBallVisual.enabled = false; //turns sprite off
    }

    public void ResetBall()
    {
        transform.SetParent(Girl.transform); //set girl as parent
        m_Rb.isKinematic = true; //set to kinematic
        m_Rb.velocity = Vector2.zero; //stop velocity
        m_Cl.enabled = false; //disable collider
        transform.localPosition = new Vector3(0,0,0);
        m_canThrow = true; 
        BearBallVisual.enabled = false; //turns sprite off
    }

    void OnStartThrow()
    {
        if (m_canThrow == false)
            return;
        m_isThrowing = true;
        m_DragStartPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        //Zooms camera out
        m_Camera.m_Lens.OrthographicSize = m_ZoomOut;

        //Stop girl movement
        Girl.GetComponent<GirlScript>().StopMovement();


        animator.SetBool("Throwing", true);
    }

    void OnCancelThrow()
    {
        m_isThrowing = false;

        m_Lr.positionCount = 0;
        m_Camera.m_Lens.OrthographicSize = m_ZoomNormal;

        //enable girl movement
        Girl.GetComponent<GirlScript>().EnableMovement();

        animator.SetBool("Throwing", false);
    }

    void OnMouseMove(InputValue value)
    {
        m_mousePosition = value.Get<Vector2>();
        OnMouseMove();
    }

    void OnMouseMove()
    {
        if (!m_isThrowing) return;

        Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 _velocity = (DragEndPos - pos) * m_Power;

        Vector2[] trajectory = Plot(m_Rb, (Vector2)transform.position, _velocity, 500);

        m_Lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = trajectory[i];
        }
        m_Lr.SetPositions(positions);
    }

    void OnEndThrow()
    {
        if (m_canThrow == false || m_isThrowing == false)
            return;
        Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 _velocity = (DragEndPos - pos) * m_Power;

        //turns sprite on
        BearBallVisual.enabled = true;



        m_canThrow = false;
        m_isThrowing = false;

        m_Rb.velocity = _velocity;
        m_Lr.positionCount = 0;
        m_Cl.enabled = true;
        m_Rb.isKinematic = false;
        transform.SetParent(null);
        //Switch to TargetGroup
        m_Camera.Follow = m_TargetGroupCamera.transform;
        m_CharacterBase.Stop();
        //enable girl movement
        Girl.GetComponent<GirlScript>().EnableMovement();
        //Switch controller to bear
        m_CharacterController.m_ControlledCharacter = m_CharacterController.m_Characters[1];

        animator.SetBool("Throwing", false);
        m_AudioManager.Play("BearThrow");
    }


    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for(int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }    
}

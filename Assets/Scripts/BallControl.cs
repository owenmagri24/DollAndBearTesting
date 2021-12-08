using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{
    public GameObject DollBear;
    public GameObject Girl;

    private float m_Power = 5f;

    private Rigidbody2D m_Rb;

    private LineRenderer m_Lr;

    private CircleCollider2D m_Cl;

    private Vector2 m_DragStartPos;

    private Vector2 m_mousePosition;

    private bool m_isThrowing = false;

    [SerializeField]
    private float camera_ZoomOut = 15f;

    [SerializeField]
    private float camera_ZoomNormal = 10f;

    private void Awake()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        m_Lr = GetComponent<LineRenderer>();
        m_Cl = GetComponent<CircleCollider2D>();

    }

    // Start is called before the first frame update
    private void Start()
    {
        m_Rb.isKinematic = true;
        m_Cl.enabled = false;
    }

    private void Update() {
        //Physics2D.IgnoreCollision(Girl.GetComponent<Collider2D>() , this.GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        /*
        Debug.Log("Points colliding: " + col.contacts.Length);
        Debug.Log("First point that collided: " + col.contacts[0].point);
        */
        DollBear.transform.position = transform.position;
        transform.SetParent(Girl.transform);

        m_Rb.isKinematic = true;
        m_Cl.enabled = false;
        m_Rb.velocity = Vector2.zero;
        this.transform.localPosition = new Vector3(0,0,0);
        DollBear.SetActive(true);

    }

    void OnStartThrow()
    {
        m_isThrowing = true;
        m_DragStartPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        Camera.main.orthographicSize = camera_ZoomOut;
        
    }

    void OnMouseMove(InputValue value)
    {
        m_mousePosition = value.Get<Vector2>();

        if (!m_isThrowing) return;

        Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        Vector2 _velocity = (DragEndPos - m_DragStartPos) * m_Power;

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
        Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(m_mousePosition);
        Vector2 _velocity = (DragEndPos - m_DragStartPos) * m_Power;

        m_isThrowing = false;

        m_Rb.velocity = _velocity;
        m_Lr.positionCount = 0;
        m_Cl.enabled = true;
        m_Rb.isKinematic = false;
        transform.SetParent(null);
        Camera.main.orthographicSize = camera_ZoomNormal;

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

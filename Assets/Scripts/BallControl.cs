using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{

    public GameObject testDot;
    public GameObject DollBear;
    public GameObject Girl;

    public float power = 5f;

    Rigidbody2D rb;

    LineRenderer lr;

    CircleCollider2D cl;

    Vector2 DragStartPos;

    Vector2 mousePosition;

    bool isThrowing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
        cl = GetComponent<CircleCollider2D>();

    }

    // Start is called before the first frame update
    private void Start()
    {
        rb.isKinematic = true;
        cl.enabled = false;

        Physics2D.IgnoreCollision(Girl.GetComponent<Collider2D>() , this.GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Points colliding: " + col.contacts.Length);
        Debug.Log("First point that collided: " + col.contacts[0].point);
        //testDot.transform.position = col.contacts[0].point;
        DollBear.transform.position = col.contacts[0].point;
        transform.SetParent(Girl.transform);

        rb.isKinematic = true;
        cl.enabled = false;
        rb.velocity = Vector2.zero;
        this.transform.localPosition = new Vector3(0,0,0);
        DollBear.SetActive(true);

    }

    void OnStartThrow()
    {
        isThrowing = true;
        DragStartPos = Camera.main.ScreenToWorldPoint(mousePosition);
        
    }

    void OnMouseMove(InputValue value)
    {
        mousePosition = value.Get<Vector2>();

        if (!isThrowing) return;

        Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 _velocity = (DragEndPos - DragStartPos) * power;

        Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

        lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = trajectory[i];
        }
        lr.SetPositions(positions);
    }

    void OnEndThrow()
    {
        Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 _velocity = (DragEndPos - DragStartPos) * power;

        isThrowing = false;

        rb.velocity = _velocity;
        lr.positionCount = 0;
        cl.enabled = true;
        rb.isKinematic = false;
        transform.SetParent(null);
        
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

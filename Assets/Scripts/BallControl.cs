using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{
    public Transform GirlParent;

    public GameObject testDot;

    public float power = 5f;

    Rigidbody2D rb;

    LineRenderer lr;

    Vector2 DragStartPos;

    Vector2 mousePosition;

    bool isThrowing = false;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Points colliding: " + col.contacts.Length);
        Debug.Log("First point that collided: " + col.contacts[0].point);
        testDot.transform.position = col.contacts[0].point;
        transform.SetParent(GirlParent);
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

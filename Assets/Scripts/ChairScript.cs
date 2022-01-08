using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChairScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_CenterOfMass;

    private Rigidbody2D m_rb;

    private void Awake() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        m_rb.centerOfMass = m_CenterOfMass;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Ball")
        {
            //change object layer to Platforms so the player can collide with it
            gameObject.layer = 6; 
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * m_CenterOfMass, 0.4f);
    }
}

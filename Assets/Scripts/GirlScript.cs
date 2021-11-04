using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlScript : MonoBehaviour
{   
    private Rigidbody2D m_Rigidbody2D;

    private float m_jumpForce = 500f;
    float girlSpeed = 5f;
    Vector2 direction = Vector2.zero;

    void Start(){
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Translate(direction * girlSpeed * Time.deltaTime);
    }

    void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    void OnJump(){
        //Debug.Log("jumped");
        m_Rigidbody2D.AddForce(new Vector2(0f, m_jumpForce));
    }
}

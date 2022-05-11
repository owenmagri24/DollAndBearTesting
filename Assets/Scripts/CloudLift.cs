using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLift : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    [SerializeField] private Transform m_TopPos;
    [SerializeField] private Transform m_BottomPos;
    [HideInInspector] public Vector2 m_StartPos;
    private Vector2 m_Direction = Vector2.up;
    
    public bool m_CanMove = false;

    private void Start() {
        m_StartPos = transform.position;
    }

    void Update()
    {
        if(m_CanMove == false) { return; }

        transform.Translate(m_Direction * m_Speed * Time.deltaTime);
        if (transform.position.y <= m_BottomPos.position.y)
        {
            m_Direction = Vector2.up;
        }
        if(transform.position.y >= m_TopPos.position.y)
        {
            m_Direction = Vector2.down;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.parent = null;
        }
    }
}

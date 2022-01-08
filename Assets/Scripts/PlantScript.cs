using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{

    [SerializeField]
    private GameObject m_TopCollision;
    private Vector3 m_TargetPosition;

    [SerializeField]
    private float m_LiftSpeed;

    [SerializeField]
    private float m_LiftHeight;

    [SerializeField]
    private Animator m_Animator;

    void Start()
    {
        m_TargetPosition = m_TopCollision.transform.position;

    }

    void FixedUpdate()
    {
        if(m_TopCollision.transform.position.y < m_TargetPosition.y )
        {
            m_TopCollision.transform.Translate(Vector3.up * m_LiftSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Glass")
        {
            m_Animator.SetBool("Growing", true);
            m_TargetPosition = new Vector3(m_TargetPosition.x, m_LiftHeight, m_TargetPosition.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCartScript : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_Offset;
    private Vector2 m_StartingPos;
    private Vector2 m_Direction = Vector2.left;
    public bool m_CanMove = false;

    void Start()
    {
        m_StartingPos = new Vector2(transform.position.x , transform.position.y);
    }

    private void Update() {
        if(!m_CanMove){ return; }

        transform.Translate(m_Direction * m_Speed * Time.deltaTime);
        if (transform.position.x <= m_StartingPos.x - m_Offset)
        {
            m_Direction = Vector2.right;
        }
        else if(transform.position.x >= m_StartingPos.x + m_Offset)
        {
            m_Direction = Vector2.left;
        }
    }
}

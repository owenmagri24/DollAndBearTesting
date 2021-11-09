using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform m_Target;
    [SerializeField]
    private Vector3 m_Offset;

    [SerializeField]
    private float m_SmoothSpeed = 0.125f;

    Vector3 velocity = Vector3.zero;

    void LateUpdate(){
        //Without SmoothSpeed
        //transform.position = new Vector3(m_Target.position.x + m_Offset.x, m_Target.position.y + m_Offset.y, m_Offset.z);

        //Using SmoothSpeed
        Vector3 mainPosition = new Vector3(m_Target.position.x + m_Offset.x, m_Target.position.y + m_Offset.y, m_Offset.z);
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, mainPosition, m_SmoothSpeed);
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, mainPosition, ref velocity, m_SmoothSpeed);
        transform.position = smoothedPosition;
    }
}

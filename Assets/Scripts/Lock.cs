using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField]
    private Transform m_PivotObject;
    [SerializeField]
    private Transform m_ObjectToRotate;
    private bool m_Locked = true;


    public void OpenChestTop()
    {
        if(m_Locked)
        {
            m_ObjectToRotate.RotateAround(m_PivotObject.position, Vector3.right, 100);
            m_Locked = false;
            m_ObjectToRotate.parent.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}

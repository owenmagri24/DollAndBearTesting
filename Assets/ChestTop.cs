using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTop : MonoBehaviour
{

    [SerializeField]
    private Transform m_PivotObject;
    [SerializeField]
    private Transform m_ObjectToRotate;
    private bool m_Locked;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bear")
        {
            Debug.Log("Entered trigger");

            m_ObjectToRotate.RotateAround(m_PivotObject.position, Vector3.right, 100 * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CushionScript : MonoBehaviour
{   
    private Transform m_CushionsParent;

    void Start()
    {
        m_CushionsParent = transform.parent;
    }

    void Update()
    {
        if(gameObject.transform.parent == null)
        {
            gameObject.transform.SetParent(m_CushionsParent);
        }
    }
}

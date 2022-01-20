using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepingBearScript : MonoBehaviour
{
    [SerializeField] private GameObject m_TextParent;

    public void ToggleText()
    {
        if(m_TextParent.activeSelf){
            m_TextParent.SetActive(false);
        }
        else
        {
            m_TextParent.SetActive(true);
        }
    }
    
}

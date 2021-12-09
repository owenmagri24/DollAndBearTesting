using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlScript : CharacterBase
{
    public override void OnInteract()
    {
        base.OnInteract();

        if(m_hit.collider != null){
            m_ObjectHit = m_hit.collider.gameObject;

            if(m_ObjectHit.tag == "Bear"){
                m_ObjectHit.SetActive(false);
            }
        }
    }
}

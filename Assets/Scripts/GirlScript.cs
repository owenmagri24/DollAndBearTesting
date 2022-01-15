using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GirlScript : CharacterBase
{
    public override void OnInteract()
    {
        base.OnInteract();
        
        if(m_ObjectHit != null &&  m_ObjectHit.tag == "Bear"){
            m_ObjectHit.SetActive(false);
        }
    }

    public void StopMovement()
    {
        //freezes Horizontal movement and Z Rotation
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void EnableMovement()
    {
        //removes constrains except Z rotation
        m_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

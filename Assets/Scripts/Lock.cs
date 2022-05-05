using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField]
    private Transform m_PivotObject;
    [SerializeField]
    private Transform m_ObjectToRotate;
    [SerializeField]
    private Material m_GreyMaterial;
    [SerializeField]
    private SpriteRenderer m_KeySprite;
    private bool m_Locked = true;
    private Animator m_Animator;


    private void Start() {
        m_Animator = GetComponent<Animator>();
    }

    public void OpenChestTop()
    {
        if(m_Locked)
        {
            m_ObjectToRotate.RotateAround(m_PivotObject.position, Vector3.right, 100);
            m_Locked = false;
            m_ObjectToRotate.parent.GetComponent<BoxCollider2D>().enabled = false;
            m_Animator.SetTrigger("Destroy");
            m_KeySprite.material = m_GreyMaterial;
        }
    }

    public void DestroyLock()
    {
        Destroy(gameObject);
    }
}

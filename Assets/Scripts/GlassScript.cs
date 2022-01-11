using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassScript : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    private float delay = 0f;

    [SerializeField]
    private AnimationClip m_DestroyAnim;
    private Rigidbody2D m_Rb;

    private void Awake() {
        m_Rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "PlantPot")
        {
            m_Rb.isKinematic = true;
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy(){
        m_Animator.SetBool("Destroy", true);
        delay = m_DestroyAnim.length;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

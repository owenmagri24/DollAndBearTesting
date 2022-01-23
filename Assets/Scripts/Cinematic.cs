using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cinematic : MonoBehaviour
{
    [SerializeField]
    private Image[] images;

    [SerializeField]
    private Animator[] animators;

    private UIManager m_UIManager;
    
    private void Awake() 
    {
        m_UIManager = FindObjectOfType<UIManager>();
    }

    void OnClick()
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(i == 6) //if first page complete
            {
                for(int o = 5; o >= 0; o--)
                {
                    Destroy(images[o]); //delete previous images
                }
            }
            if(images[i] != null && !images[i].enabled) //if image not null and not enabled
            {
                images[i].enabled = true;
                animators[i].SetBool("FadeIn", true);
                return;
            }
            else if(i == images.Length -1)
            {
                //Cinematic finished, load level
                LoadLevel();
            }
        }
    }
    
    void OnEscape()
    {
        LoadLevel();
    }

    void LoadLevel()
    {
        m_UIManager.LoadLevel();
    }
}

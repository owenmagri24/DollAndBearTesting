using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public enum PlateColor { Red, Green, Blue, Normal, Painting }
    public PlateColor m_PlateColor;
    public Material m_PlateMaterial;

    [SerializeField]
    private PaintingPuzzle m_PaintingPuzzle;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            if(m_PlateColor == PlateColor.Painting)
            {
                var bearColor = other.gameObject.GetComponent<DollScript>().m_BearColor;
                m_PaintingPuzzle.TurnOnBridge(bearColor);
            }
            else
            {
                ChangeBearColor(other.gameObject);
            }
        }
    }

    private void ChangeBearColor(GameObject bear)
    {
        bear.GetComponentInChildren<SpriteRenderer>().material = m_PlateMaterial; //Apply Material
        bear.GetComponent<DollScript>().m_BearColor = m_PlateColor; //Set variable to which color he is
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public enum PlateColor { Red, Green, Blue }

    public PlateColor plateColor;

    public Material plateMaterial;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Bear")
        {
            ChangeBearColor(other.gameObject);
        }
    }

    private void ChangeBearColor(GameObject bear)
    {
        bear.GetComponentInChildren<SpriteRenderer>().material = plateMaterial;
    }
}

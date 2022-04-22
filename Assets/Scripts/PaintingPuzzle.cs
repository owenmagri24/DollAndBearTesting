using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingPuzzle : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> m_Bridges = new List<GameObject>();

    public void TurnOnBridge(PressurePlate.PlateColor bearColor)
    {
        if(bearColor == PressurePlate.PlateColor.Red)
        {
            TurnOffBridges();
            m_Bridges[0].SetActive(true);
        }
        else if(bearColor == PressurePlate.PlateColor.Green)
        {
            TurnOffBridges();
            m_Bridges[1].SetActive(true);
        }
        else if(bearColor == PressurePlate.PlateColor.Blue)
        {
            TurnOffBridges();
            m_Bridges[2].SetActive(true);
        }
        else
        {
            TurnOffBridges();
        }
    }

    private void TurnOffBridges()
    {
        for (int i = 0; i < m_Bridges.Count; i++)
        {
            m_Bridges[i].SetActive(false);
        }
    }
}
